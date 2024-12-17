using System;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Enemy;
using Logic.Interfaces.Unity.Player;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Level.Hero
{
    public class PlayerTargetObserver : IPlayerTargetObserver, IDisposable
    {
        private const float MaxTargetDistance = 80f;
        
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _heroDisposable;
        
        private IDisposable _updateDisposable;
        private Transform _heroTransform;

        public ReactiveProperty<IEnemy> TargetRx { get; }

        public PlayerTargetObserver(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder)
        {
            _enemyProvider = enemyProvider;
            
            TargetRx = new ReactiveProperty<IEnemy>();
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnHeroCreated);
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                _updateDisposable?.Dispose();
                TargetRx.Value = null;
                
                return;
            }

            _heroTransform = hero.Transform;

            _updateDisposable = Observable
                .Interval(TimeSpan.FromSeconds(0.3f))
                .Subscribe(_ => FindNearestAliveTarget());
        }

        private void FindNearestAliveTarget()
        {
            var playerPosition = _heroTransform.position;
            
            if (TargetRx.Value != null && !TargetRx.Value.IsDead)
            {
                var distance = Vector3.SqrMagnitude(playerPosition - TargetRx.Value.Position);
                
                if (distance < MaxTargetDistance)
                {
                    return;
                }
            }

            var enemies = _enemyProvider.AliveEnemies;

            if (enemies.IsNullOrEmpty())
            {
                return;
            }
            
            var minDistance = MaxTargetDistance;
            IEnemy nearestEnemy = null;

            foreach (var enemy in enemies)
            {
                var distance = Vector3.SqrMagnitude(playerPosition - enemy.Position);

                if (distance > minDistance)
                {
                    continue;
                }
                
                minDistance = distance;
                    
                nearestEnemy = enemy;
            }

            TargetRx.Value = nearestEnemy;
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}