using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;

namespace Logic.Services.Level.Survival.Hero
{
    public class SurvivalLevelHeroDeathObserver : HeroDeathObserver
    {
        private readonly IPauseService _pauseService;
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;

        public SurvivalLevelHeroDeathObserver(
            IHeroHolder heroHolder, 
            IPauseService pauseService,
            IGameEndedPopupPresenter gameEndedPopupPresenter) 
            : base(heroHolder)
        {
            _pauseService = pauseService;
            _gameEndedPopupPresenter = gameEndedPopupPresenter;
        }

        protected override async UniTask OnHeroDie()
        {
            await base.OnHeroDie();
            
            _pauseService.Pause();
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
        }
    }
}