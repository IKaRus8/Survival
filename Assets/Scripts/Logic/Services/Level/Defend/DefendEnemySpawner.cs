using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.DefendLevel;
using Logic.Interfaces.Services.Level.Enemy;
using UnityEngine;

namespace Logic.Services.Level.Defend
{
    public class DefendEnemySpawner : EnemySpawnerBase
    {
        private readonly IEnemySpawnPointsProvider _spawnPointsProvider;

        protected override string EnemyId => Constants.Enemy.Id.NavEnemy;
        protected override string SpawnSettingsId => Constants.Settings.Spawn.DefendSpawnSettings;
        
        public DefendEnemySpawner(
            IEnemySpawnPointsProvider spawnPointsProvider,
            IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
            IEnemyFactory factory,
            IEnemyModelsProvider enemyModelsProvider,
            IAttackModelsProvider attackModelsProvider,
            IEnemyProvider enemyProvider) 
            : base(enemySpawnSettingsProvider,
            factory,
            enemyModelsProvider,
            attackModelsProvider,
            enemyProvider)
        {
            _spawnPointsProvider = spawnPointsProvider;
        }
        
        protected override Vector3 GetSpawnPosition()
        {
            return _spawnPointsProvider.GetRandomSpawnPoint();
        }
    }
}