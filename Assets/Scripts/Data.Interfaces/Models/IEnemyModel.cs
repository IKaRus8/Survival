using System;

namespace Data.Interfaces.Models
{
    public interface IEnemyModel
    {
        string Id { get; }
        float Health { get; }
        float AttackDamage { get;}
        float MoveSpeed { get; }
        TimeSpan AttackDelay { get; }
        float AttackDistance { get; }
    }
}