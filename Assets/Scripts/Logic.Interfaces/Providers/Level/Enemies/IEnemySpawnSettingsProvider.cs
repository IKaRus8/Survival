using System;

namespace Logic.Interfaces.Providers.Level.Enemies
{
    public interface IEnemySpawnSettingsProvider
    {
        TimeSpan SpawnCooldown { get; }
        
        float GetChanceForSpawn();
    }
}