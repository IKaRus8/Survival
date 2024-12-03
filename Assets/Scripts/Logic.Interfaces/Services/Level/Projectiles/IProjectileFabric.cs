using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Interfaces.Services.Level.Projectiles
{
    public interface IProjectileFabric
    {
        IProjectileFabric From(Vector3 startPosition);

        IProjectileFabric To(Vector3 endPosition);

        IProjectileFabric FindTargets(Action onCollide);

        IProjectileFabric WithSpeed(float speed);

        UniTask SpawnAsync();
    }
}