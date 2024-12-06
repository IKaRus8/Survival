using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models.Attack;
using Data.Models.Attack;
using UnityEngine;

namespace Logic.Interfaces.Services.Level.Projectiles
{
    public interface IProjectileFabric
    {
        IProjectileFabric From(Vector3 startPosition);

        IProjectileFabric To(Vector3 endPosition);

        IProjectileFabric FindTargets(Action onCollide);

        IProjectileFabric WithSpeed(float speed);

        IProjectileFabric WithAttackModel(IAttackModel model);

        UniTask SpawnAsync();
    }
}