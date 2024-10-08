using Data;
using Logic.Interfaces;
using R3;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField]
        private Transform _weaponShootPoint;
    
        //TODO: private
        public MovingSettings movingSettings;
    
        private float _currentHealth;
        private Transform _transform;
    
        public float Speed => movingSettings.BaseSpeed;
        public float RotateSpeed => movingSettings.BaseRotationSpeed;
        public float Health => _currentHealth;
        public Transform Transform => _transform;
        public Transform WeaponShootPoint => _weaponShootPoint;
        public bool IsDead => _currentHealth <= 0;
        public ReactiveProperty<bool> IsRotating { get; } = new(false);
    
        //TODO: отдельно в характеристики
        public float Armor { get; }
        public float DamageResistance { get; }
        public float DamageReflection { get; }
        public float Vampirism { get; }

        private void Awake()
        {
            _transform = transform;
        
            _currentHealth = 100f;
        }

        public void Move(Vector3 direction, float speed, float delta)
        {
            if (IsDead)
            {
                return;
            }

            _transform.position += direction * speed * delta;
        }

        public void Rotate(Vector3 direction, float speed, float delta)
        {
            if (IsDead)
            {
                return;
            }

            var rotationAngle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        
            var rotation = Quaternion.Euler(0, rotationAngle * speed * delta, 0);
        
            _transform.rotation *= rotation;
        }


        public void Die()
        {
            _currentHealth = 0;
        }

        public void TakeDamage(IDamageble attacker, float damage)
        {
            _currentHealth -= damage;
        
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float healAmount)
        {
            _currentHealth += healAmount;
        
            _currentHealth = Mathf.Clamp(_currentHealth, 0, 100);
        }
    }
}