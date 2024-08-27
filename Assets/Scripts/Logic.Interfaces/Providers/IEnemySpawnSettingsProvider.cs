using System.Collections.Generic;
using Data.ScriptableObjects;

namespace Logic.Interfaces.Providers
{
    public interface IEnemySpawnSettingsProvider
    {
        List<EnemySpawnSettings.SpawnParameter> GetAllParameters();
    }
}