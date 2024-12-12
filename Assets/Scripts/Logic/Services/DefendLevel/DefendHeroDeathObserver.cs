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

        protected override UniTask OnHeroDie()
        {
            return base.OnHeroDie();
        }
    }
}