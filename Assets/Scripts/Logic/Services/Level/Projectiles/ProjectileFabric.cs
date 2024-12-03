using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.RuntimeData;
using Logic.Services.Level.Pools;
using UnityEngine;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectileFabric : IProjectileFabric
    {
        private readonly ProjectilesPool _projectilesPool;
        private readonly IProjectilesProvider _projectilesProvider;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _speed;

        public ProjectileFabric(
            ProjectilesPool projectilesPool,
            IProjectilesProvider projectilesProvider)
        {
            _projectilesPool = projectilesPool;
            _projectilesProvider = projectilesProvider;
        }

        public IProjectileFabric From(Vector3 startPosition)
        {
            _startPosition = startPosition;
            
            return this;
        }

        public IProjectileFabric To(Vector3 endPosition)
        {
            _endPosition = endPosition;
            
            return this;
        }

        public IProjectileFabric FindTargets(Action onCollide)
        {
            
            
            return this;
        }

        public IProjectileFabric WithSpeed(float speed)
        {
            _speed = speed;
            
            return this;
        }

        public async UniTask SpawnAsync()
        {
            var projectile = _projectilesPool.Spawn();
            _projectilesProvider.AddProjectile(projectile);
            
            var direction = (_endPosition - _startPosition).normalized;

            projectile.ProjectileDamage = new DamageModel(10f);
            projectile.Speed = _speed;
            projectile.Move(_startPosition, direction);
        }
    }
}