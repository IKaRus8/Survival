using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services.Player;
using Logic.Services.Level;

namespace Logic.Services.DefendLevel
{
    public class DefendHeroDeathObserver : HeroDeathObserver
    {
        public DefendHeroDeathObserver(IHeroHolder heroHolder) : base(heroHolder)
        {
        }

        protected override async UniTask OnHeroDie()
        {
            base.OnHeroDie().Forget();

            await UniTask.Delay(3000);

            _heroHolder.CreateHero();
        }
    }
}