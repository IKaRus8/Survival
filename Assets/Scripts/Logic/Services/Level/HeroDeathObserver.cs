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
        private readonly CompositeDisposable _disposables;

        private IHero _hero;

        public event Action HeroDie;

        protected HeroDeathObserver(
            IHeroHolder heroHolder)
        {
            _disposables = new CompositeDisposable();
            
            heroHolder.HeroRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _hero = hero;
            
            Observable.EveryUpdate().Subscribe(CheckIsPlayerDead).AddTo(_disposables);
        }

        private void CheckIsPlayerDead(Unit _)
        {
            if (_hero.Health > 0f)
            {
                return;
            }
            
            OnHeroDie().Forget();
        }

        protected virtual async UniTask OnHeroDie()
        {
            Dispose();

            HeroDie?.Invoke();

            await _hero.Die();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}