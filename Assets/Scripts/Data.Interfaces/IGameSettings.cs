using System.Collections.Generic;
using Data.Models.Enemy;

namespace Data.Interfaces
{
    public interface IGameSettings
    {
        IReadOnlyCollection<EnemySpawnParameter> EnemySpawnParameters { get; }
    }
}