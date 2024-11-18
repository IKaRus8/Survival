using System;
using Logic.Interfaces;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Providers.Enemies;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Player
{
    public class PlayerTargetObserver : IPlayerTargetObserver, IDisposable
    {
        private const float MaxTargetDistance = 100f;
        
        private readonly IEnemyProvider _enemyProvider;
        private readonly CompositeDisposable _disposables;
        
        private Transform _playerTransform;

        public ReactiveProperty<IEnemy> TargetRx { get; }

        public PlayerTargetObserver(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder)
        {
            _enemyProvider = enemyProvider;
            
            TargetRx = new ReactiveProperty<IEnemy>();
            _disposables = new CompositeDisposable();
            
            heroHolder.HeroRx.Subscribe(InitTargetsObserve).AddTo(_disposables);
        }

        private void InitTargetsObserve(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _playerTransform = hero.Transform;

            Observable.Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => FindNearestAliveTarget())
                .AddTo(_disposables);
        }

        private void FindNearestAliveTarget()
        {
            var playerPosition = _playerTransform.position;
            
            if (TargetRx.Value != null && !TargetRx.Value.IsDead)
            {
                var distance = Vector3.SqrMagnitude(playerPosition - TargetRx.Value.EnemyTransform.position);
                
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
                var distance = Vector3.SqrMagnitude(playerPosition - enemy.EnemyTransform.position);

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