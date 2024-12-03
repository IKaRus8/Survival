using Logic.Interfaces.Unity.Projectiles;
using Logic.RuntimeData;
using UnityEngine;

namespace Logic.Unity.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        private Vector3 _direction;
        private Transform _transform;
        
        public DamageModel ProjectileDamage { get; set; }
        public float Speed { get; set; }
        public Vector3 Position => _transform.position;
        public bool IsActive { get; private set; }

        private void Awake()
        {
            _transform = transform;
        }

        public void Active()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void Move(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            
            _direction = direction;
        }

        private void Update()
        {
            if (_direction == Vector3.zero)
            {
                return;
            }
            
            transform.position += _direction * Speed * Time.deltaTime;
        }
    }
}