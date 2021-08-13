using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PathTracer
{
    public class OrenNayar : BxDF
    {
        private Spectrum kd;
        private double roughness;
        public OrenNayar(Spectrum albedo, double rough)
        {
            kd = albedo;
            roughness = rough;
        }

        public override Spectrum f(Vector3 wo, Vector3 wi)
        {
            var r = Math.Sqrt(wo.x * wo.x + wo.y * wo.y + wo.z * wo.z); // this is 1, so r can be ignored

            //caluclate angelses, source are lectures
            //  // wx = r* sinTheta * cosphi
            // wy = r * sintheta * sinphi
            // wz = r ? cos tehta
            // for wo
            var cosThetaWo = Utils.CosTheta(wo);
            var thetaWo = Math.Acos(cosThetaWo);

            var sinThetaWo = Utils.SinTheta(wo);
            var cosPhiWo = wo.x / sinThetaWo;
            var phiWo = Math.Acos(cosPhiWo);
            // for wi
            var cosThetaWi = Utils.CosTheta(wi);
            var thetaWi = Math.Acos(cosThetaWi);

            var sinThetaWi = Utils.SinTheta(wi);
            var cosPhiWi = wi.x / sinThetaWi;
            var phiWi = Math.Acos(cosPhiWi);

            // calculate alpha and beta 
            var alpha = Math.Max(thetaWi, thetaWo);
            var betha = Math.Min(thetaWi, thetaWo);


            var A = 1 - ((roughness * roughness) / (2 * (roughness * roughness + 0.33)));
            var B = 0.45 * (roughness * roughness) / (roughness * roughness + 0.09);
            
            var freturn = (kd / Math.PI) * (A + B* Math.Max(0, Math.Cos (phiWi - phiWo)) * Math.Sin(alpha)*Math.Tan(betha));
            return freturn;
            
        }

        public override (Spectrum, Vector3, double) Sample_f(Vector3 wo)
        {
            var wi = Samplers.CosineSampleHemisphere();
            if (wo.z < 0)
                wi.z *= -1;
            double pdf = Pdf(wo, wi);
            return (f(wo, wi), wi, pdf);
        }

        public override double Pdf(Vector3 wo, Vector3 wi)
        {
            if (!Utils.SameHemisphere(wo, wi))
                return 0;

            return Math.Abs(wi.z) * Utils.PiInv; // wi.z == cosTheta
        }
    }
}
