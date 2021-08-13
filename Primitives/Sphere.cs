using System;
using MathNet.Numerics.Integration;

namespace PathTracer
{
   
  class Sphere : Shape
  {
    private String emmision = "outside";
    public double Radius { get; set; }
    public Sphere(double radius, Transform objectToWorld, String Emmision = "outside")
    {
      emmision = Emmision; 
      Radius = radius;
      ObjectToWorld = objectToWorld;
    }

    public override (double?, SurfaceInteraction) Intersect(Ray ray)
    {
      Ray r = WorldToObject.Apply(ray);

            // modefied from https://github.com/mmp/pbrt-v3/blob/master/src/shapes/sphere.cpp?fbclid=IwAR3uyrOe6LfgXYfClBAsp7KsDWG_QjEeQFV8YwfPqoVfheyTUvjFh-STRII
            // source https://www.pbr-book.org/3ed-2018/Shapes/Spheres
            //Initialize _double_ ray coordinate values

            double ox = r.o.x; double oy = r.o.y; double oz = r.o.z;
            double dx = r.d.x; double dy = r.d.y; double dz = r.d.z;

            double a = dx * dx + dy * dy + dz * dz;
            double b = 2 * (dx * ox + dy * oy + dz * oz);
            double c = ox * ox + oy * oy + oz * oz - Radius * Radius;


            // Solve quadratic equation for _t_ values

            (bool isQuadratic, double t0, double t1) = Utils.Quadratic(a, b, c);
            if (!isQuadratic) return (null, null);

            //  Check quadric shape _t0_ and _t1_ for nearest intersection
            if (t1 <= 0) // kaj je Tmax ?
                return (null, null);
            
            double tShapeHit = t0;
            if (tShapeHit <= 0) {
                tShapeHit = t1;
            }

            // TODO: Compute sphere hit position and $\phi$
            var  pHit = r.Point(tShapeHit);

            // TODO: Return shape hit and surface interaction
            var dpdu = new Vector3(-pHit.y, pHit.x, 0);
            SurfaceInteraction surfaceInteraction = new SurfaceInteraction(pHit, pHit, -r.d, dpdu, this);
            

            return (tShapeHit, ObjectToWorld.Apply(surfaceInteraction));
    }

    public override (SurfaceInteraction, double) Sample()
    {
            // TODO: Implement Sphere sampling

          var pObj = new Vector3(0, 0, 0) + Radius * Samplers.UniformSampleSphere();

          
          var normal = ObjectToWorld.ApplyNormal(pObj);
            if (emmision == "inside") {
                normal = -normal; 
            }
          var dpdu = new Vector3(-pObj.y, pObj.x, 0);
          var pdf = 1 / Area();
          return (ObjectToWorld.Apply(new SurfaceInteraction(pObj, normal, Vector3.ZeroVector, dpdu, this)), pdf);
    }

    public override double Area() { return 4 * Math.PI * Radius * Radius; }

    public override double Pdf(SurfaceInteraction si, Vector3 wi)
    {
      throw new NotImplementedException();
    }

  }
}
