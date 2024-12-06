using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using DG.Tweening;
using Logic.Interfaces.Unity;
using Logic.Services.Level.Attack;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class Hero : MonoBehaviour, IHero
    {
        private const float RotateDuration = 0.1f;
        
        [SerializeField]
        private Transform _weaponShootPoint;
        
        private float _currentHealth;
        private Transform _transform;
        private AttackProcessView _attackProcessView;
        private Tween _currentRotationTween;

        public string Id => Constants.Hero.Id.SimpleHero;
        public float Speed => Model.Speed;
        public float Health => _currentHealth;
        public Transform Transform => _transform;
        public Transform WeaponShootPoint => _weaponShootPoint;
        public IHeroModel Model { get; private set; }
        public IAttackModel HeroAttackModel { get; private set; }
        public bool IsDead => _currentHealth <= 0f;

        private void Awake()
        {
            _transform = transform;
        }

        public void Initialize(IHeroModel model, IAttackModel attackModel)
        {
            Model = model;
            HeroAttackModel = attackModel;

            _currentHealth = Model.Health;
            
            _attackProcessView = new AttackProcessView(attackModel.AttackDelay);
        }

        public void Move(Vector3 direction)
        {
            var newPosition = transform.position + (direction * Speed * Time.deltaTime);
            
            MoveTo(newPosition);
        }

        public void Rotate(Vector3 direction)
        {
            // Останавливаем текущую анимацию вращения, если она есть
            _currentRotationTween?.Kill();

            // Нормализуем направление, чтобы избежать проблем с масштабами вектора
            //direction.y = 0; // Игнорируем вертикальный компонент
            direction.Normalize();

            // Вычисляем целевой угол поворота
            var rotationAngle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            var targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
    
            // Запускаем плавный поворот
            _currentRotationTween = _transform.DORotateQuaternion(targetRotation, RotateDuration);
        }

        public virtual async UniTask Attack()
        {
            await _attackProcessView.Attack();
        }

        public void Die()
        {
            _currentHealth = 0;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0f)
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
            _attackProcessView.CancelAttack();
        }

        private void MoveTo(Vector3 newPosition)
        {
            _transform.position = newPosition;
        }
    }
}