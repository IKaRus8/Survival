using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Logic.Interfaces;
using UnityEngine;

namespace Logic.Unity.Enemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private readonly TimeSpan _attackDelay;

        private bool _isCanAttack;

        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }
        public Transform Transform => transform;

        //TODO: прокинуть в спавнере
        public IEnemyModel Model { get; private set; }

        private void OnEnable()
        {
            CurrentHealth = Model.Health;

            IsDead = false;
            _isCanAttack = true;
        }

        public void Init(IEnemyModel model)
        {
            Model = model;
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
            damageSystem.DoDamage(this, player, Model.AttackDamage);

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
            CurrentHealth = Math.Clamp(CurrentHealth, 0, Model.Health);
        }
    }
}