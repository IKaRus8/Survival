using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Services.DefendLevel
{
    public class DefendObjectDeathObserver : IDisposable
    {
        private readonly IDefendObject _defendObject;
        private readonly IPauseService _pauseService;
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;
        private readonly IDisposable _updateDisposable;

        public DefendObjectDeathObserver(
            IDefendObject defendObject,
            IPauseService pauseService,
            IGameEndedPopupPresenter gameEndedPopupPresenter)
        {
            _defendObject = defendObject;
            _pauseService = pauseService;
            _gameEndedPopupPresenter = gameEndedPopupPresenter;

            _updateDisposable = Observable.EveryUpdate().Subscribe(CheckIsDefendObjectDead);
        }

        private void CheckIsDefendObjectDead(Unit _)
        {
            if (_defendObject.Health > 0f)
            {
                return;
            }
            
            OnDefendObjectDie().Forget();
        }

        protected virtual async UniTask OnDefendObjectDie()
        {
            _updateDisposable?.Dispose();
            
            _pauseService.Pause();
            
            await _defendObject.Die();

            _gameEndedPopupPresenter.ShowPopup();
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}