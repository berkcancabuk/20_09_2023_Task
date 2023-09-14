using UnityEngine;

namespace Controller
{
    public class Pointer3D
    {
        public float x;
        public float y;
        public float z;

        public Pointer3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 ConvertVector3D()
        {
            return new Vector3(x, y, z);
        }
        
        public Pointer3D Plus(Pointer3D pointer)
        {
            return new Pointer3D(x+ pointer.x, y+ pointer.y, z+ pointer.z);
        }
        
        public static Pointer3D ConvertVectorToPointer3D(Vector3 vector)
        {
            return new Pointer3D(vector.x, vector.y, vector.z);
        }

    }
    
}