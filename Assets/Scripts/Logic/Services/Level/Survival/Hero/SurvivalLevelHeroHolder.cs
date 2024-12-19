using Data.Interfaces.Constants;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Services.Level.Hero;

namespace Logic.Services.Level.Survival.Hero
{
    public class SurvivalLevelHeroHolder : HeroHolder
    {
        protected override string HeroId => Constants.Hero.Id.SimpleHero;
        
        public SurvivalLevelHeroHolder(IHeroSpawner heroSpawner) : base(heroSpawner)
        {
        }
    }
}