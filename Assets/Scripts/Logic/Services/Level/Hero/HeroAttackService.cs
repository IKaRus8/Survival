using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Hero
{
    public class HeroAttackService : IDisposable
    {
        private readonly TimeSpan _shotDelay = TimeSpan.FromSeconds(0.5f);
        
        private readonly ReactiveProperty<IEnemy> _targetRx;
        private readonly IProjectileFabric _projectileFabric;
        private readonly IDamageSystem _damageSystem;
        private readonly IDisposable _heroDisposable;

        private Transform _shotPoint;
        private IDisposable _attackDisposable;
        private IHero _hero;

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
            _projectileFabric
                .From(_shotPoint.position)
                .To(_targetRx.Value.EnemyTransform.position)
                .BySeconds((float)_hero.Model.AttackDelay.TotalSeconds)
                .Spawn();
            
            var damage = await _hero.Attack();

            if (damage <= 0)
            {
                return;
            }
            
            _damageSystem.ToEnemy(_targetRx.Value).Do(damage);
        }

        private bool CanAttack()
        {
            return _targetRx.Value != null && !_targetRx.Value.IsDead;
        }
        
        public void Dispose()
        {
            _attackDisposable?.Dispose();
            _heroDisposable?.Dispose();
        }
    }
}