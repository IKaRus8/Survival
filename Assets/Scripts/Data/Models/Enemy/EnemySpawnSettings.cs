using System;
using Utilities.Extensions;

namespace Data.Models.Enemy
{
    public struct EnemySpawnSettings
    {
        public string Id { get; }
        public TimeSpan SpawnCooldown { get; }
        public EnemySpawnParameter[] Parameters { get; }
        
        public bool IsEmpty => Id.IsNullOrEmpty();
        
        public EnemySpawnSettings(
            string id,
            float spawnCooldown, 
            EnemySpawnParameter[] parameters)
        {
            Id = id;
            Parameters = parameters;
            SpawnCooldown = TimeSpan.FromSeconds(spawnCooldown);
        }
    }
}