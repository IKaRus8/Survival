using System;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;

namespace Logic.Services.Level.Attack
{
    public class DamageSystem : IDamageSystem, IDisposable
    {
        private readonly IDisposable _playerDisposable;
        
        private IHero _hero;
        private IDamageable _target;

        public DamageSystem(IHeroHolder heroHolder)
        {
            _playerDisposable = heroHolder.HeroRx.Subscribe(OnHeroExist);
        }
        
        public IDamageSystem FromHero()
        {


            return this;
        }

        public IDamageSystem FromEnemy()
        {
            
            
            return this;
        }

        public IDamageSystem ToTarget(IDamageable target)
        {
            _target = target;
            
            return this;
        }

        public IDamageSystem ToHero()
        {
            _target = _hero;
            
            return this;
        }

        public void Do(float damage)
        {
            if (_target == null)
            {
                return;
            }
            
            _target.TakeDamage(damage);
        }

        private void OnHeroExist(IHero hero)
        {
            _hero = hero;
        }

        public void Dispose()
        {
            _playerDisposable?.Dispose();
        }
    }
}