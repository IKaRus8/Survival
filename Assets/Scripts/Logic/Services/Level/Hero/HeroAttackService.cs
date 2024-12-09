using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Hero
{
    public class HeroAttackService : IPauseHandler, IDisposable
    {
        private readonly ReactiveProperty<IEnemy> _targetRx;
        private readonly IProjectileFabric _projectileFabric;
        private readonly IDisposable _heroDisposable;
        private CancellationTokenSource _cancellationTokenSource;

        private Transform _shotPoint;
        private IHero _hero;

        public HeroAttackService(
            IHeroHolder heroHolder,
            IPlayerTargetObserver targetProvider,
            IProjectileFabric projectileFabric)
        {
            _projectileFabric = projectileFabric;
            _cancellationTokenSource = new CancellationTokenSource();
            
            _targetRx = targetProvider.TargetRx;
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        public void Pause()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        public void Resume()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var token = _cancellationTokenSource.Token;
            
            AttackProcess(token).Forget();
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _hero = hero;
            _shotPoint = hero.WeaponShootPoint;
            
            Resume();
        }

        private async UniTaskVoid AttackProcess(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested == false)
            {
                if (!CanAttack())
                {
                    await UniTask.Yield();
                    
                    continue;
                }

                await Attack();
            }
        }

        private async UniTask Attack()
        {
            await _hero.AttackPrepare();
            
            // Запуск анимации выстрела или снаряда
            _projectileFabric
                .From(_shotPoint.position)
                .To(_targetRx.Value.Position)
                .WithAttackModel(_hero.HeroAttackModel)
                .SpawnAsync()
                .Forget();

            // Выполнение атаки
            await _hero.Attack();
        }

        private bool CanAttack()
        {
            return _targetRx.Value != null 
                   && !_targetRx.Value.IsDead;
        }
        
        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _cancellationTokenSource?.Dispose();
        }
    }
}