using UnityEngine;

namespace Logic.Unity.Weapon
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 10f;
        
        public void Initialize(Vector3 position)
        {
            transform.position = position;
        }

        private void Update1()
        {
            transform.position += transform.forward * (Speed * Time.deltaTime);
        }
    }
}