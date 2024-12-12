using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Player;

namespace Logic.Services.Level.Hero
{
    public class LevelHeroDeathObserver : HeroDeathObserver
    {
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;

        public LevelHeroDeathObserver(
            IHeroHolder heroHolder, 
            IGameEndedPopupPresenter gameEndedPopupPresenter) 
            : base(heroHolder)
        {
            _gameEndedPopupPresenter = gameEndedPopupPresenter;
        }

        protected override async UniTask OnHeroDie()
        {
            await base.OnHeroDie();
            
            _gameEndedPopupPresenter.ShowPopup().Forget();
        }
    }
}