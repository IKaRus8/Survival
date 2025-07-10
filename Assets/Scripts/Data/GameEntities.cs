using System.Collections.Generic;
using Data.Interfaces;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using Data.Models;
using Data.Models.Attack;
using Data.Models.Enemy;

namespace Data
{
    public class GameEntities : IGameEntities
    {
        public IReadOnlyCollection<IEnemyModel> EnemyModels { get; }
        public IReadOnlyCollection<IHeroModel> HeroModels { get; }
        public IReadOnlyCollection<IAttackModel> AttackModels { get; }

        public GameEntities()
        {
            EnemyModels = GetEnemyModels();
            HeroModels = GetHeroModels();
            AttackModels = GetAttackModels();
        }

        private static IEnemyModel[] GetEnemyModels()
        {
            return new IEnemyModel[]
            {
                new EnemyModel(
                    Constants.Enemy.Id.SimpleEnemy,
                    50f,
                    1.5f,
                    Constants.Attack.SimpleMelee),

                new EnemyModel(
                    Constants.Enemy.Id.NavEnemy,
                    60f,
                    1.5f,
                    Constants.Attack.BigMelee),
            };
        }

        private static IHeroModel[] GetHeroModels()
        {
            return new IHeroModel[]
            {
                new HeroModel(
                    Constants.Hero.Id.SimpleHero,
                    300f,
                    4f,
                    Constants.Attack.SimpleRange),
                
                new HeroModel(
                    Constants.Hero.Id.DefendHero,
                    300f,
                    3f,
                    Constants.Attack.LongRange),
            };
        }

        private static IAttackModel[] GetAttackModels()
        {
            return new IAttackModel[]
            {
                new MeleeAttackModel(
                    Constants.Attack.SimpleMelee,
                    10f,
                    1f,
                    1.5f),

                new RangeAttackModel(
                    Constants.Attack.SimpleRange,
                    30f,
                    1.1f,
                    15f,
                    13f,
                    Constants.Vfx.ElectroHit),

                new MeleeAttackModel(
                    Constants.Attack.BigMelee,
                    20f,
                    0.9f,
                    3.5f),

                new RangeAttackModel(
                    Constants.Attack.LongRange,
                    20f,
                    0.8f,
                    40f,
                    15f,
                    Constants.Vfx.ElectroHit),
            };
        }
    }
}