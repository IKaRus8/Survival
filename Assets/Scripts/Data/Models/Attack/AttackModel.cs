using System;
using Data.Interfaces.Models.Attack;

namespace Data.Models.Attack
{
    public abstract class AttackModel : IAttackModel
    {
        public string ID { get; }
        public float Damage { get; }
        public TimeSpan AttackDelay { get; }
        public float SqrAttackDistance { get; }

        protected AttackModel(
            string id, 
            float damage, 
            float attackSpeed,
            float attackDistance)
        {
            ID = id;
            Damage = damage;
            AttackDelay = TimeSpan.FromSeconds(1 / attackSpeed);
            SqrAttackDistance = attackDistance * attackDistance;
        }
    }
}