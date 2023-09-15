using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace View
{
    public class Nodes : MonoBehaviour
    {
        public Vector3 position;
        public int gCost;
        public int hCost;
        public Nodes parent;
        public bool isObstacle; // Yeni eklenen özellik

        public int FCost
        {
            get { return gCost + hCost; }
        }

        public Nodes(Vector3 _pos)
        {
            position = _pos;
            isObstacle = false; // Varsayılan olarak bir düğüm engel değil.
        }
    }
}