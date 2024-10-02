using Data.Interfaces.Models;
using UnityEngine;

namespace Logic.Interfaces
{
    public interface IEnemy : IDamageble
    {
        Transform Transform { get; }
        bool IsDead { get; }
        float CurrentHealth { get; }
        IEnemyModel Model { get; }

        void Die();
        void Reset();
        void Move(Vector3 offset);
        void Attack(IPlayer player, IDamageSystem damageSystem);
    }
}