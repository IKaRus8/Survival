using UnityEngine;

namespace Logic.Unity.Weapon
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 10f;
        
        public void Initialize(
            Vector3 position,
            Vector3 rotation)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
        }

        private void Update()
        {
            transform.position += transform.forward * (Speed * Time.deltaTime);
        }
    }
}