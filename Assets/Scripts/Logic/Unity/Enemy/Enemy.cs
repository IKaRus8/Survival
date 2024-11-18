using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Logic.Interfaces;
using Logic.Interfaces.Unity;
using Logic.Services.Level;
using UnityEngine;
using Zenject;

namespace Logic.Unity.Enemy
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        private CancellationTokenSource _attackCancellationTokenSource;
        private UniTaskCompletionSource _currentAttackCompletionSource;
        private AttackModule _attackModule;

        public abstract string Id { get; }
        public float Health { get; private set; }
        public bool IsDead => Health <= 0;
        public IEnemyModel Model { get; private set; }
        public Transform EnemyTransform { get; private set; }

        [Inject]
        private void Construct()
        {
            
        }

        protected virtual void Awake()
        {
            EnemyTransform = transform;
        }

        public void Initialize(IEnemyModel model)
        {
            Model = model;
            _attackModule = new AttackModule(Model.AttackDamage, Model.AttackDelay);
        }

        public virtual void Move(Vector3 offset)
        {
            MoveTo(EnemyTransform.position + offset);
        }

        public void MoveTo(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public virtual async UniTask<float> Attack()
        {
            return await _attackModule.Attack();
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        public void Heal(float healAmount)
        {
            var health = Math.Min(Health + healAmount, Model.Health);
            
            Health = health;
        }

        public virtual void Reset()
        {
            Health = Model.Health;
            
            gameObject.SetActive(true);
        }

        public virtual void Die()
        {
            Health = 0f;
            
            gameObject.SetActive(false);
        }

        public void CancelAttack()
        {
            _attackModule.CancelAttack();
        }

        private void OnDestroy()
        {
            _attackCancellationTokenSource?.Cancel();
            _attackCancellationTokenSource?.Dispose();
        }
    }
}