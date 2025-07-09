using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Level;

namespace Logic.Services.Level
{
    public class GameOverService : IGameOverService
    {
        private readonly IPauseService _pauseService;
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;

        public GameOverService(
            IPauseService pauseService,
            IGameEndedPopupPresenter gameEndedPopupPresenter)
        {
            _pauseService = pauseService;
            _gameEndedPopupPresenter = gameEndedPopupPresenter;
        }

        public void LevelComplete()
        {
            _pauseService.Pause();
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
        }

        public void LevelFailed()
        {
            _pauseService.Pause();
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
        }
    }
}