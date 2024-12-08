using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroDeathObserver : IHeroDeathObserver, IDisposable
    {
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;
        private readonly CompositeDisposable _disposables;
        
        private IHero _hero;

        public event Action HeroDie;

        public HeroDeathObserver(
            IHeroHolder heroHolder,
            IGameEndedPopupPresenter gameEndedPopupPresenter)
        {
            _gameEndedPopupPresenter = gameEndedPopupPresenter;
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
            
            GameEnd().Forget();
        }

        private async UniTaskVoid GameEnd()
        {
            Dispose();
            
            HeroDie?.Invoke();
            
            await _hero.Die();
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}