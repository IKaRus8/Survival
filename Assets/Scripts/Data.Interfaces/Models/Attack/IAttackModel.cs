using System;

namespace Data.Interfaces.Models.Attack
{
    public interface IAttackModel
    {
        string ID { get; }
        float Damage { get; }
        TimeSpan AttackDelay { get; }
        float SqrAttackDistance { get; }
    }
}