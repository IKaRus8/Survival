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

        private IEnemyModel[] GetEnemyModels()
        {
            return new IEnemyModel[]
            {
                new EnemyModel(
                    Constants.Enemy.Id.SimpleEnemy,
                    50f,
                    1f,
                    Constants.Attack.SimpleMelee),
            };
        }

        private IHeroModel[] GetHeroModels()
        {
            return new IHeroModel[]
            {
                new HeroModel(
                    Constants.Hero.Id.SimpleHero,
                    300,
                    5f,
                    Constants.Attack.SimpleRange)
            };
        }

        private IAttackModel[] GetAttackModels()
        {
            return new IAttackModel[]
            {
                new MeleeAttackModel(
                    Constants.Attack.SimpleMelee,
                    10f,
                    1f),
                
                new RangeAttackModel(
                    Constants.Attack.SimpleRange,
                    30f,
                    0.5f,
                    25f,
                    5f,
                    Constants.Vfx.ElectroHit)
            };
        }
    }
}