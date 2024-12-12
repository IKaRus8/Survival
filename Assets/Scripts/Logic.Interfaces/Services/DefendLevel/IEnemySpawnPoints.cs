using UnityEngine;

namespace Logic.Interfaces.Services.DefendLevel
{
    public interface IEnemySpawnPoints
    {
        Transform[] SpawnPoints { get; }
    }
}