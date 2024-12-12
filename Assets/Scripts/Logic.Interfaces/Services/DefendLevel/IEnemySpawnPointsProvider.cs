using UnityEngine;

namespace Logic.Interfaces.Services.DefendLevel
{
    public interface IEnemySpawnPointsProvider
    {
        Vector3 GetRandomSpawnPoint();
    }
}