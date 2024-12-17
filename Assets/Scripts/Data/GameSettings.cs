using System.Collections.Generic;
using Data.Interfaces;
using Data.Interfaces.Constants;
using Data.Models.Enemy;

namespace Data
{
    public class GameSettings : IGameSettings
    {
        public IReadOnlyCollection<EnemySpawnSettings> SpawnSettings { get; }

        public GameSettings()
        {
            SpawnSettings = GetEnemySpawnParameters();
        }

        private EnemySpawnSettings[] GetEnemySpawnParameters()
        {
            return new[]
            {
                new EnemySpawnSettings(
                    Constants.Settings.Spawn.LevelSpawnSettings,
                    0.5f,
                    new[]
                    {
                        new EnemySpawnParameter(30, 100),
                        new EnemySpawnParameter(50, 60),
                        new EnemySpawnParameter(80, 10)
                    }
                ),
                
                new EnemySpawnSettings(
                    Constants.Settings.Spawn.DefendSpawnSettings,
                    2f,
                    new[]
                    {
                        new EnemySpawnParameter(10, 100),
                        new EnemySpawnParameter(15, 30),
                        new EnemySpawnParameter(20, 3)
                    }
                ),
            };
        }
    }
}