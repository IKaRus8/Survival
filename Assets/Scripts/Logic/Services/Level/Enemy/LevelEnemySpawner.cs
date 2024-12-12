using System;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class LevelEnemySpawner : EnemySpawnerBase
    {
        private readonly IGridSystem _gridSystem;

        protected override TimeSpan Cooldown => TimeSpan.FromSeconds(0.5f);

        public LevelEnemySpawner(
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