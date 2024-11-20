using UnityEngine;

namespace Logic.RuntimeData
{
    public class GridRectangle : Rectangle
    {
        public int Index { get; private set; }
        
        public GridRectangle(int index, Vector3[] positions) : base(positions)
        {
            Index = index;
        }

        public GridRectangle(int index, Vector3 point, float radius) : base(point, radius)
        {
            Index = index;
        }
    }
}