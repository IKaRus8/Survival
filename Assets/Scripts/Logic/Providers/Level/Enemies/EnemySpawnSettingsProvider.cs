using System.Linq;
using Data.Interfaces;
using Data.Models.Enemy;
using Logic.Interfaces.Providers.Level.Enemies;

namespace Logic.Providers.Level.Enemies
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly EnemySpawnParameter[] _spawnParameters;

        public EnemySpawnSettingsProvider(
            IEnemyProvider enemyProvider,
            IGameSettings gameSettings)
        {
            _enemyProvider = enemyProvider;

            _spawnParameters = gameSettings.EnemySpawnParameters.OrderBy(p => p.Quantity).ToArray();
        }

        public float GetChanceForSpawn()
        {
            var enemyCount = _enemyProvider.AliveEnemyCount;

            foreach (var enemyParameter in _spawnParameters)
            {
                if (enemyCount < enemyParameter.Quantity)
                {
                    return enemyParameter.Chance;
                }
            }

            return 0f;
        }
    }
}