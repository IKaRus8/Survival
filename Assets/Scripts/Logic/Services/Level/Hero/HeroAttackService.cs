using System;
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
    public class HeroAttackService : IDisposable
    {
        private readonly TimeSpan _shotDelay = TimeSpan.FromSeconds(1f);
        
        private readonly ReactiveProperty<IEnemy> _targetRx;
        private readonly IProjectileFabric _projectileFabric;
        private readonly IDamageSystem _damageSystem;
        private readonly IDisposable _heroDisposable;

        private Transform _shotPoint;
        private IDisposable _attackDisposable;
        private IHero _hero;
        private UniTask<float> _attackTask;
        private bool _isAttacking; // Флаг, чтобы отслеживать статус атаки

        public HeroAttackService(
            IHeroHolder heroHolder,
            IPlayerTargetObserver targetProvider,
            IProjectileFabric projectileFabric,
            IDamageSystem damageSystem)
        {
            _projectileFabric = projectileFabric;
            _damageSystem = damageSystem;
            
            _targetRx = targetProvider.TargetRx;
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _hero = hero;
            _shotPoint = hero.WeaponShootPoint;
            
            _attackDisposable = Observable.Interval(_shotDelay).Subscribe(TryAttack);
        }

        public void TryAttack(Unit _)
        {
            if (!CanAttack())
            {
                return;
            }

            Attack().Forget();
        }

        private async UniTask Attack()
        {
            _isAttacking = true; // Устанавливаем флаг начала атаки

            try
            {
                // Запуск анимации выстрела или снаряда
                _projectileFabric
                    .From(_shotPoint.position)
                    .To(_targetRx.Value.Position)
                    .WithSpeed(6f)
                    //.BySeconds((float)_hero.Model.AttackDelay.TotalSeconds)
                    .SpawnAsync()
                    .Forget();

                // Выполнение атаки
                await _hero.Attack();
            }
            finally
            {
                _isAttacking = false; // Сбрасываем флаг после завершения
            }
        }

        private bool CanAttack()
        {
            return _targetRx.Value != null 
                   && !_targetRx.Value.IsDead
                   && !_isAttacking;
        }
        
        public void Dispose()
        {
            _attackDisposable?.Dispose();
            _heroDisposable?.Dispose();
        }
    }
}