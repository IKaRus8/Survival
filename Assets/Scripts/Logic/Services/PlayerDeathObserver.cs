using Logic.Interfaces;
using R3;
using Logic.Interfaces.Presenters;

namespace Logic.Services
{
    public class PlayerDeathObserver
    {
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;
        private readonly CompositeDisposable _disposables;
        
        private IPlayer _player;

        public PlayerDeathObserver(
            IPlayerHolder playerHolder,
            IGameEndedPopupPresenter gameEndedPopupPresenter)
        {
            _gameEndedPopupPresenter = gameEndedPopupPresenter;
            _disposables = new CompositeDisposable();
            
            playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }

        private void OnPlayerCreated(IPlayer player)
        {
            if (player == null)
            {
                return;
            }

            _player = player;
            
            Observable.EveryUpdate().Subscribe(CheckIsPlayerDead).AddTo(_disposables);
        }

        private void CheckIsPlayerDead(Unit _)
        {
            if (_player.IsDead)
            {
                _gameEndedPopupPresenter.ShowPopup();
            }
        }
    }
}