using System;
using Data.Interfaces.Models;

namespace Data.Models.Enemy
{
    public class EnemyModel : IEnemyModel
    {
        public string Id { get; }
        public float Health { get; }
        public float AttackDamage { get;}
        public float MoveSpeed { get; }
        public TimeSpan AttackDelay { get; }
        public float AttackDistance { get; }
        
        public EnemyModel(
            string id,
            float attackDistance, 
            float attackDelay, 
            float moveSpeed,
            float attackDamage,
            float health)
        {
            AttackDistance = attackDistance;
            AttackDelay = TimeSpan.FromSeconds(attackDelay);
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            Health = health;
            Id = id;
        }
    }
}