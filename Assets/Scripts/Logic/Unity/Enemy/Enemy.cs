using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Logic.Interfaces.Unity.Enemy;
using Logic.Services.Level.Attack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Enemy
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        private const float RotateDuration = 0.1f;
        
        private static readonly int _attack = Animator.StringToHash("attack");
        private static readonly int _die = Animator.StringToHash("die");

        [SerializeField, Required]
        private EnemyViewController _viewController;
        [SerializeField, Required]
        private Animator _animator;
        
        private CancellationTokenSource _attackCancellationTokenSource;
        private UniTaskCompletionSource _currentAttackCompletionSource;
        private EnemyAttackProcessView _attackProcessView;
        private Transform _transform;
        private bool _isAttackProcess;
        private TweenerCore<Quaternion,Quaternion,NoOptions> _currentRotationTween;

        public abstract string Id { get; }
        public float Health { get; private set; }
        public bool IsDead { get; private set; }

        public bool IsAttack => _isAttackProcess;
        public IEnemyModel Model { get; private set; }

        public IAttackModel EnemyAttackModel { get; private set; }
        public bool Active => gameObject.activeSelf;
        public Vector3 Position => _transform.position;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        public void Initialize(IEnemyModel model, IAttackModel attackModel)
        {
            Model = model;
            EnemyAttackModel = attackModel;
            
            _attackProcessView = new EnemyAttackProcessView(attackModel.AttackDelay, _viewController);
        }

        public virtual void Move(Vector3 offset)
        {
            MoveTo(Position + offset);
        }

        public void MoveTo(Vector3 newPosition)
        {
            transform.position = newPosition;
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
            _isAttackProcess = true;
            _animator.SetTrigger(_attack);
    
            // Задержка перед запуском снаряда
            await UniTask.Delay(400);
        }

        public virtual async UniTask Attack()
        {
            await _attackProcessView.Attack();
            
            _isAttackProcess = false;
            _animator.ResetTrigger(_attack);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            
            Blink(Color.white);
        }

        public void Heal(float healAmount)
        {
            var health = Math.Min(Health + healAmount, Model.Health);
            
            Health = health;
            
            Blink(Color.green);
        }

        public virtual void ReInitialize()
        {
            Health = Model.Health;
            IsDead = false;
            _animator.ResetTrigger(_die);
            
            gameObject.SetActive(true);
        }

        public virtual async UniTask Die()
        {
            Health = 0f;
            IsDead = true;
            CancelAttack();

            _animator.SetTrigger(_die);
            
            await UniTask.Delay(1000);
            
            gameObject.SetActive(false);
        }

        public void CancelAttack()
        {
            _isAttackProcess = false;
            
            _attackProcessView.CancelAttack();
        }

        private void Blink(Color blinkColor)
        {
            _viewController.Blink(blinkColor);
        }

        private void OnDestroy()
        {
            _attackCancellationTokenSource?.Cancel();
            _attackCancellationTokenSource?.Dispose();
        }
    }
}