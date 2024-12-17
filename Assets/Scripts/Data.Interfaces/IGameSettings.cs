using System.Collections.Generic;
using Data.Models.Enemy;

namespace Data.Interfaces
{
    public interface IGameSettings
    {
        IReadOnlyCollection<EnemySpawnSettings> SpawnSettings { get; }
    }
}