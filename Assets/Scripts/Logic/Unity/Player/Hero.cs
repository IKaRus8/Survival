using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using DG.Tweening;
using Logic.Interfaces.Unity;
using Logic.Services.Level.Attack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class Hero : MonoBehaviour, IHero
    {
        private const float RotateDuration = 0.2f;
        
        private static readonly int _attack = Animator.StringToHash("attack");
        private static readonly int _move = Animator.StringToHash("move");
        private static readonly int _die = Animator.StringToHash("die");

        [SerializeField, Required]
        private Transform _weaponShootPoint;
        [SerializeField, Required]
        private Animator _animator;
        
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
            if (direction == Vector3.zero)
            {
                _animator.SetBool(_move, false);
                
                return;
            }
            
            _animator.SetBool(_move, true);
            
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

        public virtual async UniTask AttackPrepare()
        {
            _animator.SetTrigger(_attack);
    
            // Задержка перед запуском снаряда
            await UniTask.Delay(200);
        }

        public virtual async UniTask Attack()
        {
            // Задержка для завершения атаки
            await UniTask.Delay(HeroAttackModel.AttackDelay);
            
            _animator.ResetTrigger(_attack);
        }

        public async UniTask Die()
        {
            _currentHealth = 0;
            
            _animator.ResetTrigger(_attack);
            _animator.SetTrigger(_die);
            
            await UniTask.Delay(2500);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
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