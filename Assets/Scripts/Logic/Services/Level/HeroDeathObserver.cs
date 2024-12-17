using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Services.Level
{
    public abstract class HeroDeathObserver : IHeroDeathObserver, IDisposable
    {
        protected readonly IHeroHolder _heroHolder;
        private readonly IDisposable _heroDisposable;

        private IDisposable _updateDisposable;
        private IHero _hero;

        public event Action HeroDie;

        protected HeroDeathObserver(IHeroHolder heroHolder)
        {
            _heroHolder = heroHolder;
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                _updateDisposable?.Dispose();
                    
                return;
            }

            _hero = hero;
            
            _updateDisposable = Observable.EveryUpdate().Subscribe(CheckIsHeroDead);
        }

        private void CheckIsHeroDead(Unit _)
        {
            if (_hero.Health > 0f)
            {
                return;
            }
            
            OnHeroDie().Forget();
        }

        protected virtual async UniTask OnHeroDie()
        {
            _heroHolder.HeroDie();
            HeroDie?.Invoke();

            await _hero.Die();
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}