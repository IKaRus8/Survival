using Data.Models.Attack;
using UnityEngine;

namespace Logic.Interfaces.Unity.Projectiles
{
    public interface IProjectile
    {
        float Damage { get; }
        float Speed { get; }
        Vector3 Position { get; }
        bool IsActive { get; }
        string DestroyVfxId { get; }

        void Initialization(
            RangeAttackModel attackModel,
            Vector3 startPosition,
            Vector3 direction);
    }
}