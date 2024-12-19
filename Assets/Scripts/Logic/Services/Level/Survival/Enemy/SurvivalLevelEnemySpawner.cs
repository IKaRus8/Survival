using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using UnityEngine;

namespace Logic.Services.Level.Survival.Enemy
{
    public class SurvivalLevelEnemySpawner : EnemySpawnerBase
    {
        private readonly IGridSystem _gridSystem;

        protected override string EnemyId => Constants.Enemy.Id.SimpleEnemy;
        protected override string SpawnSettingsId => Constants.Settings.Spawn.LevelSpawnSettings;

        public SurvivalLevelEnemySpawner(
            IGridSystem gridSystem,
            IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
            IEnemyFactory factory,
            IEnemyModelsProvider enemyModelsProvider,
            IAttackModelsProvider attackModelsProvider,
            IEnemyProvider enemyProvider) 
            : base(
            enemySpawnSettingsProvider,
            factory,
            enemyModelsProvider,
            attackModelsProvider,
            enemyProvider)
        {
            _gridSystem = gridSystem;
        }

        protected override Vector3 GetSpawnPosition()
        {
            var gridElementRectangle = _gridSystem.GetRandomGridPlaneWithOutHero().ElementRectangle;

            return gridElementRectangle.RandomPosition;
        }
    }
}