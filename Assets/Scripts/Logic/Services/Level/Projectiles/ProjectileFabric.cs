using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models.Attack;
using Data.Models.Attack;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Services.Level.Pools;
using UnityEngine;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectileFabric : IProjectileFabric
    {
        private readonly ProjectilesPool _projectilesPool;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _speed;
        private RangeAttackModel _attackModel;

        public ProjectileFabric(ProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
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

        public IProjectileFabric WithAttackModel(IAttackModel model)
        {
            _attackModel = model as RangeAttackModel;

            return this;
        }

        public async UniTask SpawnAsync()
        {
            var direction = (_endPosition - _startPosition).normalized;
            
            _projectilesPool.Spawn(_attackModel, _startPosition, direction);
        }
    }
}