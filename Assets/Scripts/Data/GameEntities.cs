using System.Collections.Generic;
using Data.Interfaces;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Data.Models;
using Data.Models.Enemy;

namespace Data
{
    public class GameEntities : IGameEntities
    {
        public IReadOnlyCollection<IEnemyModel> EnemyModels { get; }
        public IReadOnlyCollection<IHeroModel> HeroModels { get; }

        public GameEntities()
        {
            EnemyModels = GetEnemyModels();
            HeroModels = GetHeroModels();
        }

        private IEnemyModel[] GetEnemyModels()
        {
            return new IEnemyModel[]
            {
                new EnemyModel(
                    Constants.Enemy.Id.SimpleEnemy,
                    3f,
                    1f,
                    5f,
                    10f,
                    100f),
                new EnemyModel(
                    Constants.Enemy.Id.SimpleEnemy + "2",
                    3f,
                    10f,
                    5f,
                    100f,
                    100f)
            };
        }

        private IHeroModel[] GetHeroModels()
        {
            return new IHeroModel[]
            {
                new HeroModel(
                    Constants.Hero.Id.SimpleHero,
                    100,
                    10f,
                    5f,
                    2f,
                    1f)
            };
        }
    }
}