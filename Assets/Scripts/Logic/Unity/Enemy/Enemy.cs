using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Unity.Enemy
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        private bool _isCanAttack;
        private IDamageSystem _damageSystem;

        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }
        public abstract IEnemyModel Model { get; protected set; }
        public Transform EnemyTransform { get; private set; }

        [Inject]
        private void Construct(IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;
        }

        protected virtual void Awake()
        {
            EnemyTransform = transform;
        }

        public virtual void Die()
        {
            IsDead = true;
            CurrentHealth = 0f;
            
            gameObject.SetActive(false);
        }

        public virtual void Move(Vector3 offset)
        {
            MoveTo(EnemyTransform.position + offset);
        }

        public void MoveTo(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public virtual async UniTask Attack(IDamageable target)
        {
            if (!_isCanAttack)
            {
                return;
            }

            _isCanAttack = false;

            await AttackPrepare();
            AttackProcess(target);
            await PostAttack();

            _isCanAttack = true;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
        }

        public void Heal(float healAmount)
        {
            var health = Math.Min(CurrentHealth + healAmount, Model.Health);
            
            CurrentHealth = health;
        }

        public virtual void Reset()
        {
            CurrentHealth = Model.Health;

            IsDead = false;
            _isCanAttack = true;
            
            gameObject.SetActive(true);
        }

        private async UniTask AttackPrepare()
        {
            await UniTask.Delay(Model.AttackDelay);
        }

        private void AttackProcess(IDamageable target)
        {
            _damageSystem.FromEnemy().ToPlayer();
        }

        private async UniTask PostAttack()
        {
            await UniTask.Delay(Model.AttackDelay);
        }
    }
}