using System;

namespace Data.Interfaces.Models
{
    public interface IHeroModel
    {
        string Id { get; }
        float Health { get; }
        float Speed { get; }
        float RotateSpeed { get; }
        float AttackDamage { get; }
        TimeSpan AttackDelay { get; }
    }
}