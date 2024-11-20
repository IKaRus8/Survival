using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroDeathObserver : IDisposable
    {
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;
        private readonly CompositeDisposable _disposables;
        
        private IHero _hero;

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
            if (!_hero.IsDead)
            {
                return;
            }
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
            
            Dispose();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}