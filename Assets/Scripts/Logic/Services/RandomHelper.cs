using UnityEngine;

namespace Logic.Services
{
    public class RandomHelper
    {
        public static float GetRandomFloat()
        {
            return Random.Range(0f, 1f);
        }
    }
}