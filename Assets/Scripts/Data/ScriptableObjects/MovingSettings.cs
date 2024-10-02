using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MovingSettings", menuName = "Survival/MovingSettings", order = 0)]
    public class MovingSettings : ScriptableObject
    {
        [SerializeField]
        private float _baseSpeed;
        [SerializeField]
        private float _baseRotationSpeed;

        public float BaseSpeed => _baseSpeed;
        public float BaseRotationSpeed => _baseRotationSpeed;
    }
}