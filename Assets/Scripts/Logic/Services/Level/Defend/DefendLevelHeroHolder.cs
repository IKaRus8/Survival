using Data.Interfaces.Constants;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Survival.Hero;

namespace Logic.Services.Level.Defend
{
    public class DefendLevelHeroHolder : HeroHolder
    {
        protected override string HeroId => Constants.Hero.Id.DefendHero;
        
        public DefendLevelHeroHolder(IHeroSpawner heroSpawner) : base(heroSpawner)
        {
        }
    }
}