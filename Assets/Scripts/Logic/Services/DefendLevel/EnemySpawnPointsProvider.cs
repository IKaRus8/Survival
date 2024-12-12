using Logic.Interfaces.Services.DefendLevel;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.DefendLevel
{
    public class EnemySpawnPointsProvider : IEnemySpawnPointsProvider
    {
        private readonly IEnemySpawnPoints _spawnPoints;

        public EnemySpawnPointsProvider(IEnemySpawnPoints spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        public Vector3 GetRandomSpawnPoint()
        {
            return _spawnPoints.SpawnPoints.RandomElement().position;
        }
    }
}