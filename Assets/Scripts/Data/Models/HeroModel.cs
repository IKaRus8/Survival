using System;
using Data.Interfaces.Models;

namespace Data.Models
{
    public class HeroModel : IHeroModel
    {
        public string Id { get; }
        public float Health { get; }
        public float Speed { get; }
        public float RotateSpeed { get; }
        public TimeSpan AttackDelay { get; }
        public float AttackDamage { get; }
        
        public HeroModel(
            string id,
            float health,
            float attackDamage, 
            float speed, 
            float rotateSpeed,
            float attackSpeed)
        {
            AttackDamage = attackDamage;
            RotateSpeed = 1f / rotateSpeed;
            Speed = speed;
            Health = health;
            Id = id;
            
            AttackDelay = TimeSpan.FromSeconds(1f / attackSpeed);
        }
    }
}