using Data.Models;
using Logic.RuntimeData;
using UnityEngine;

namespace Logic.Interfaces.Unity.Projectiles
{
    public interface IProjectile
    {
        DamageModel ProjectileDamage { get; set; }
        float Speed { get; set; }
        Vector3 Position { get; }
        bool IsActive { get; }

        void Move(Vector3 startPosition, Vector3 direction);
    }
}