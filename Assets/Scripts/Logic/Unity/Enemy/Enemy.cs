using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using Logic.Interfaces.Unity.Enemy;
using Logic.Services.Level.Attack;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Logic.Unity.Enemy
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField, Required]
        private EnemyViewController _viewController;
        
        private CancellationTokenSource _attackCancellationTokenSource;
        private UniTaskCompletionSource _currentAttackCompletionSource;
        private EnemyAttackProcessView _attackProcessView;
        private Transform _transform;
        private bool _isAttackProcess;

        public abstract string Id { get; }
        public float Health { get; private set; }
        public bool IsDead { get; private set; }

        public bool CanAttack => !_isAttackProcess;
        public IEnemyModel Model { get; private set; }

        public IAttackModel EnemyAttackModel { get; private set; }
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

        public virtual async UniTask Attack()
        {
            _isAttackProcess = true;
            
            await _attackProcessView.Attack();
            
            _isAttackProcess = false;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            
            Blink(Color.red);
        }

        public void Heal(float healAmount)
        {
            var health = Math.Min(Health + healAmount, Model.Health);
            
            Health = health;
            
            Blink(Color.green);
        }

        public virtual void Reset()
        {
            Health = Model.Health;
            IsDead = false;
            
            gameObject.SetActive(true);
        }

        public virtual void Die()
        {
            Health = 0f;
            IsDead = true;
            
            gameObject.SetActive(false);
        }

        public void CancelAttack()
        {
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