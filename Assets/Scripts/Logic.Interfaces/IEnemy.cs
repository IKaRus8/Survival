using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using UnityEngine;

namespace Logic.Interfaces
{
    public interface IEnemy : IDamageable
    {
        Transform EnemyTransform { get; }
        bool IsDead { get; }
        float CurrentHealth { get; }
        IEnemyModel Model { get; }

        void Die();
        void Reset();
        void Move(Vector3 offset);
        void MoveTo(Vector3 newPosition);
        UniTask Attack(IDamageable target);
        void TakeDamage(float damage);
    }
}