using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Logic.Interfaces;
using Logic.Interfaces.Unity;
using Logic.Services.Level;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class Hero : MonoBehaviour, IHero
    {
        [SerializeField]
        private Transform _weaponShootPoint;
        
        private float _currentHealth;
        private Transform _transform;
        private AttackModule _attackModule;

        public string Id => Constants.Hero.Id.SimpleHero;
        public float Speed => Model.Speed;
        public float Health => _currentHealth;
        public Transform Transform => _transform;
        public Transform WeaponShootPoint => _weaponShootPoint;
        public IHeroModel Model { get; private set; }
        public bool IsDead => _currentHealth <= 0;

        private void Awake()
        {
            _transform = transform;
        }

        public void Initialize(IHeroModel model)
        {
            Model = model;

            _currentHealth = Model.Health;
            
            _attackModule = new AttackModule(Model.AttackDamage, Model.AttackDelay);
        }

        public void Move(Vector3 direction)
        {
            MoveTo(direction * Speed * Time.deltaTime);
        }

        public void Rotate(Vector3 direction)
        {
            var rotationAngle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);

            var rotation = Quaternion.Euler(0, rotationAngle * Model.RotateSpeed, 0);

            _transform.rotation *= rotation;
        }
        
        public virtual async UniTask<float> Attack()
        {
            return await _attackModule.Attack();
        }

        public void Die()
        {
            _currentHealth = 0;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(float healAmount)
        {
            var newHealth = _currentHealth + healAmount;

            _currentHealth = Mathf.Min(newHealth, Model.Health);
        }

        public void CancelAttack()
        {
            _attackModule.CancelAttack();
        }

        private void MoveTo(Vector3 newPosition)
        {
            _transform.position = newPosition;
        }
    }
}