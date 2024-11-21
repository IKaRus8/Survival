using UnityEngine;

namespace Logic.Services
{
    public class RandomHelper
    {
        public static float GetChance()
        {
            return GetRandomFloat(0f, 1f);
        }

        public static float GetRandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static Vector3 GetRandomVector(float maxValue)
        {
            return new Vector3(GetRandomFloat(-maxValue, maxValue), 0f, GetRandomFloat(-maxValue, maxValue));
        }

        public static Color GetRandomColor()
        {
            return new Color(GetChance(), GetChance(), GetChance());
        }

        public static Vector3 GetRandomizedVector(Vector3 vector, float maxOffset)
        {
            var x = vector.x * GetRandomFloat(1 - maxOffset, maxOffset);
            var y = vector.y * GetRandomFloat(1 - maxOffset, maxOffset);
            var z = vector.z * GetRandomFloat(1 - maxOffset, maxOffset);
            
            return new Vector3(x, y, z);
        }
    }
}