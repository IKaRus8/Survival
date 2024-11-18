using System.Linq;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.RuntimeData
{
    public class Rectangle
    {
        private float _weight;
        private float _height;

        public Vector3 MinPoint { get; }
        public Vector3 MaxPoint { get; }
        public Vector3 RandomPosition => GetRandomPosition();

        public Rectangle(Vector3[] positions)
        {
            if (positions.IsNullOrEmpty())
            {
                Debug.LogWarning("positions for rectangle is empty");
                
                return;
            }
            
            var minX = positions.Min(p => p.x);
            var maxX = positions.Max(p => p.x);
            var minY = positions.Min(p => p.y);
            var maxY = positions.Max(p => p.y);

            MinPoint = new Vector3(minX, minY);
            MaxPoint = new Vector3(maxX, maxY);

            _weight = maxX - minX;
            _height = maxY - minY;
        }

        public Rectangle(Vector3 point, float radius)
        {
            MinPoint = GetMinPointByRadius(point, radius);
            MaxPoint = GetMaxPointByRadius(point, radius);

            _weight = _height = radius * 2f;
        }

        private static Vector3 GetMinPointByRadius(Vector3 point, float radius)
        {
            if (radius == 0f)
            {
                return point;
            }
            
            var minX = point.x - radius;
            var minY = point.y - radius;
            
            return new Vector3(minX, minY);
        }

        private static Vector3 GetMaxPointByRadius(Vector3 point, float radius)
        {
            if (radius == 0f)
            {
                return point;
            }
            
            var maxX = point.x + radius;
            var maxY = point.y + radius;
            
            return new Vector3(maxX, maxY);
        }

        private Vector3 GetRandomPosition()
        {
            var randomX = Random.Range(MinPoint.x, MaxPoint.x);
            var randomZ = Random.Range(MinPoint.z, MaxPoint.z);
            
            return new Vector3(randomX, 0, randomZ);
        }
    }
}