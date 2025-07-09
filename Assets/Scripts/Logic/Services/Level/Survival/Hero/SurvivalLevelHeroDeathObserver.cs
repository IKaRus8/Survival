using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;

namespace Logic.Services.Level.Survival.Hero
{
    public class SurvivalLevelHeroDeathObserver : HeroDeathObserver
    {
        private readonly IGameOverService _gameOverService;

        public SurvivalLevelHeroDeathObserver(
            IHeroHolder heroHolder, 
            IGameOverService gameOverService) 
            : base(heroHolder)
        {
            _gameOverService = gameOverService;
        }

        protected override async UniTask OnHeroDie()
        {
            await base.OnHeroDie();
            
            _gameOverService.LevelFailed();
        }
    }
}