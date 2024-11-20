using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Services.Level.Pools;
using UnityEngine;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectileFabric : IProjectileFabric
    {
        private readonly BulletPool _bulletPool;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float _duration;

        public ProjectileFabric(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
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
            
            
            return this;
        }

        public IProjectileFabric BySeconds(float seconds)
        {
            _duration = seconds;
            
            return this;
        }

        public void Spawn()
        {
            var bullet = _bulletPool.Spawn();

            bullet.transform.DOMove(_endPosition, _duration).From(_startPosition);
        }

        public UniTask SpawnAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}