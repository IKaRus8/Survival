using Data.Interfaces.Models;

namespace Data.Models
{
    public class EnemyModel : IEnemyModel
    {
        public string Id { get; }
        public float Health { get; }
        public float AttackDamage { get;}
        public float MoveSpeed { get; }
        public float AttackDelay { get; }
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
            AttackDelay = attackDelay;
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            Health = health;
            Id = id;
        }
    }
}