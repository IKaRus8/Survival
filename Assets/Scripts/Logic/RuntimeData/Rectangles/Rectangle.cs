using UnityEngine;

namespace Logic.RuntimeData.Rectangles
{
    public class Rectangle
    {
        public static Rectangle BaseRectangle => new(Vector3.zero, 0f);

        public Vector3 MinPoint { get; }
        public Vector3 MaxPoint { get; }
        public Vector3 CenterPoint { get; }
        public Vector3 RandomPosition => GetRandomPosition();

        public Rectangle(Vector3 point, float radius)
        {
            CenterPoint = point;
            
            MinPoint = GetMinPointByRadius(point, radius);
            MaxPoint = GetMaxPointByRadius(point, radius);
        }

        private static Vector3 GetMinPointByRadius(Vector3 point, float radius)
        {
            if (radius == 0f)
            {
                return point;
            }
            
            var minX = point.x - radius;
            var minZ = point.z - radius;
            
            return new Vector3(minX, 0f, minZ);
        }

        private static Vector3 GetMaxPointByRadius(Vector3 point, float radius)
        {
            if (radius == 0f)
            {
                return point;
            }
            
            var maxX = point.x + radius;
            var maxZ = point.z + radius;
            
            return new Vector3(maxX, 0f, maxZ);
        }

        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(MinPoint.x, MaxPoint.x);
            var randomZ = Random.Range(MinPoint.z, MaxPoint.z);
            
            return new Vector3(randomX, 0f, randomZ);
        }
    }
}