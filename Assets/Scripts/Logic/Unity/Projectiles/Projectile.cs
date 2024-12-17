using Data.Models.Attack;
using Logic.Interfaces.Unity.Projectiles;
using UnityEngine;

namespace Logic.Unity.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        private Vector3 _direction;
        private Transform _transform;
        private float _lifeTime;
        private float _maxLifeTime;
        
        public float Damage { get; private set; }
        public float Speed { get; private set; }
        public Vector3 Position => _transform.position;
        public bool IsActive {get; private set;}
        public string DestroyVfxId { get; private set; }

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

        public void Initialization(
            RangeAttackModel attackModel,
            Vector3 startPosition,
            Vector3 direction)
        {
            Damage = attackModel.Damage;
            Speed = attackModel.ProjectileSpeed;
            DestroyVfxId = attackModel.DestroyVfx;
            _maxLifeTime = attackModel.ProjectileLifeTime;
            
            Move(startPosition, direction);
        }

        public void Move(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            
            _direction = direction;
        }

        private void Update()
        {
            _transform.position += _direction * (Speed * Time.deltaTime);
        }
    }
}