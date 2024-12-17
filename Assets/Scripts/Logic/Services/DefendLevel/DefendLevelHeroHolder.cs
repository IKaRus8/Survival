using Data.Interfaces.Constants;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Services.Level.Hero;

namespace Logic.Services.DefendLevel
{
    public class DefendLevelHeroHolder : HeroHolder
    {
        protected override string HeroId => Constants.Hero.Id.DefendHero;
        
        public DefendLevelHeroHolder(IHeroSpawner heroSpawner) : base(heroSpawner)
        {
        }
    }
}