using System.Collections.Generic;
using Data.Interfaces;
using Data.Models.Enemy;

namespace Data
{
    public class GameSettings : IGameSettings
    {
        public IReadOnlyCollection<EnemySpawnParameter> EnemySpawnParameters { get; }

        public GameSettings()
        {
            EnemySpawnParameters = GetEnemySpawnParameters();
        }

        private EnemySpawnParameter[] GetEnemySpawnParameters()
        {
            return new[]
            {
                new EnemySpawnParameter(30, 100),
                new EnemySpawnParameter(50, 60),
                new EnemySpawnParameter(80, 10)
            };
        }
    }
}