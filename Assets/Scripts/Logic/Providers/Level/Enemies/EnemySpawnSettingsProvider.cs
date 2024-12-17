using System;
using System.Linq;
using Data.Interfaces;
using Data.Models.Enemy;
using Logic.Interfaces.Providers.Level.Enemies;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Providers.Level.Enemies
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly EnemySpawnParameter[] _spawnParameters;
        
        public TimeSpan SpawnCooldown { get; }

        public EnemySpawnSettingsProvider(
            IEnemyProvider enemyProvider,
            IGameSettings gameSettings,
            string spawnSettingsId)
        {
            _enemyProvider = enemyProvider;

            var settings = gameSettings.SpawnSettings
                .FirstOrDefault(p => p.Id == spawnSettingsId);

            if (settings.IsEmpty)
            {
                Debug.LogError($"Could not find spawn parameter with id: {spawnSettingsId}");
                
                return;
            }
            
            SpawnCooldown = settings.SpawnCooldown;
            
            _spawnParameters = settings.Parameters
                .OrderBy(p => p.Quantity).ToArray();
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