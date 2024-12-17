using Data.Interfaces.Models;

namespace Data.Models.Enemy
{
    public struct EnemyModel : IEnemyModel
    {
        public string Id { get; }
        public float Health { get; }
        public float MoveSpeed { get; }
        public string AttackModelId { get; }
        
        public EnemyModel(
            string id,
            float health,
            float moveSpeed,
            string attackModelId)
        {
            MoveSpeed = moveSpeed;
            AttackModelId = attackModelId;
            Health = health;
            Id = id;
        }
    }
}