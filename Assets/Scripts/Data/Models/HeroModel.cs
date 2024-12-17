using Data.Interfaces.Models;

namespace Data.Models
{
    public struct HeroModel : IHeroModel
    {
        public string Id { get; }
        public float Health { get; }
        public float Speed { get; }
        public string AttackModelId { get; }
        
        public HeroModel(
            string id,
            float health,
            float speed, 
            string attackModelId)
        {
            Speed = speed;
            AttackModelId = attackModelId;
            Health = health;
            Id = id;
        }
    }
}