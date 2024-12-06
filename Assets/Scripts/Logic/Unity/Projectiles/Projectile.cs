using Data.Models.Attack;
using Logic.Interfaces.Unity.Projectiles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField, Required]
        private TrailRenderer _trailRenderer;
        
        private Vector3 _direction;
        private Transform _transform;
        
        public float Damage { get; private set; }
        public float Speed { get; private set; }
        public Vector3 Position => _transform.position;
        public bool IsActive => gameObject.activeSelf;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _trailRenderer.enabled = true;
        }

        private void OnDisable()
        {
            _trailRenderer.enabled = false;
        }

        public void Initialization(
            RangeAttackModel attackModel,
            Vector3 startPosition,
            Vector3 direction)
        {
            Damage = attackModel.Damage;
            Speed = attackModel.ProjectileSpeed;
            
            Move(startPosition, direction);
        }

        public void Move(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            
            _direction = direction;
        }

        private void Update()
        {
            transform.position += _direction * (Speed * Time.deltaTime);
        }
    }
}