using System;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Level.Hero
{
    public class PlayerTargetObserver : IPlayerTargetObserver, IDisposable
    {
        private const float MaxTargetDistance = 60f;
        
        private readonly IEnemyProvider _enemyProvider;
        private readonly CompositeDisposable _disposables;
        
        private Transform _heroTransform;

        public ReactiveProperty<IEnemy> TargetRx { get; }

        public PlayerTargetObserver(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder)
        {
            _enemyProvider = enemyProvider;
            
            TargetRx = new ReactiveProperty<IEnemy>();
            _disposables = new CompositeDisposable();
            
            heroHolder.HeroRx.Subscribe(OnHeroCreated).AddTo(_disposables);
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _heroTransform = hero.Transform;

            Observable.Interval(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ => FindNearestAliveTarget())
                .AddTo(_disposables);
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
            _disposables?.Dispose();
        }
    }
}