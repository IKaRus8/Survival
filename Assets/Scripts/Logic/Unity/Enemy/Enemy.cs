using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Unity.Enemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private const float StartHealth = 100;
        private const float Damage = 10f;

        private readonly TimeSpan _attackDelay = TimeSpan.FromSeconds(0.5f);

        private bool _isCanAttack;

        public float AttackDistance => 2.5f;
        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }
        public float MoveSpeed { get; private set; }
        public float Armor { get; }
        public float DamageResistance { get; }
        public float DamageReflection { get; }
        public float Vampirism { get; }
        public Transform Transform => transform;

        private void OnEnable()
        {
            CurrentHealth = StartHealth;
        
            IsDead = false;
            _isCanAttack = true;
        
            MoveSpeed = Random.Range(1, 3f);
        }

        public void Die()
        {
            gameObject.SetActive(false);
            IsDead = true;
        }

        //TODO: to newPosition
        public void Move(Vector3 offset)
        {
            transform.position += offset;
        }

        public virtual void Attack(IPlayer player, IDamageSystem damageSystem)
        {
            if (!_isCanAttack)
            {
                return;
            }

            _isCanAttack = false;

            AttackPrepare(player, damageSystem).Forget();
        }

        private async UniTaskVoid AttackPrepare(IPlayer player, IDamageSystem damageSystem)
        {
            await UniTask.Delay(_attackDelay);

            AttackProcess(player, damageSystem);
        }

        private void AttackProcess(IPlayer player, IDamageSystem damageSystem)
        {
            //TODO: to damage system
            damageSystem.TakeDamage(this, player, Damage);

            PostAttack().Forget();
        }

        private async UniTask PostAttack()
        {
            await UniTask.Delay(_attackDelay);

            _isCanAttack = true;
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public void TakeDamage(IDamageble attacker, float damage)
        {
            CurrentHealth -= damage;
        }
    
        public void Heal(float healAmount)
        {
            CurrentHealth += healAmount;
            CurrentHealth= Math.Clamp(CurrentHealth, 0, StartHealth);
        }
    }
}