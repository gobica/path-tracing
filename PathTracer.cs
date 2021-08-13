<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PathTracer.Samplers;

namespace PathTracer
{
  class PathTracer
  {
    //implemeantation from lecture's pseudocode
    public Spectrum Li(Ray r, Scene s)
    {
      var L = Spectrum.ZeroSpectrum; //sevalonst
      var beta = Spectrum.Create(1); 
      var nbounces = 0;
            while (nbounces < 20)
            {
                var (distance, intersection) = s.Intersect(r);
                if (intersection == null) break; 

                Vector3 wo = -r.d;

                if (intersection.Obj is Light)
                {
                    if (nbounces == 0) L = beta * intersection.Le(wo); 
                    break;
                }
                var Ld = Light.UniformSampleOneLight(intersection, s);
                L.AddTo(beta * Ld);
                (Spectrum f, Vector3 wiW, double pdf, bool isSpecular) = ((Shape)intersection.Obj).BSDF.Sample_f(wo, intersection);

              
                beta = beta * f * Utils.AbsCosTheta(wiW) / pdf;
                r = intersection.SpawnRay(wiW);

                if (nbounces > 3)
                {
                    double q = 1.0 - beta.Max();
                    if (ThreadSafeRandom.NextDouble() < q)
                    {
                        break;
                    }
                    beta = beta / (1 - q);
                }
                nbounces++;

            }
            
            return L;
    }

  }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PathTracer.Samplers;

namespace PathTracer
{
  class PathTracer
  {
    //implemeantation from lecture's pseudocode
    public Spectrum Li(Ray r, Scene s)
    {
      var L = Spectrum.ZeroSpectrum; //sevalonst
      var beta = Spectrum.Create(1); 
      var nbounces = 0;
            while (nbounces < 20)
            {
                var (distance, intersection) = s.Intersect(r);
                if (intersection == null) break; 

                Vector3 wo = -r.d;

                if (intersection.Obj is Light)
                {
                    if (nbounces == 0) L = beta * intersection.Le(wo); 
                    break;
                }
                var Ld = Light.UniformSampleOneLight(intersection, s);
                L.AddTo(beta * Ld);
                (Spectrum f, Vector3 wiW, double pdf, bool isSpecular) = ((Shape)intersection.Obj).BSDF.Sample_f(wo, intersection);

              
                beta = beta * f * Utils.AbsCosTheta(wiW) / pdf;
                r = intersection.SpawnRay(wiW);

                if (nbounces > 3)
                {
                    double q = 1.0 - beta.Max();
                    if (ThreadSafeRandom.NextDouble() < q)
                    {
                        break;
                    }
                    beta = beta / (1 - q);
                }
                nbounces++;

            }
            
            return L;
    }

  }
}
>>>>>>> d8435c6512d02224d4d346563817524cb2e9a0f2
