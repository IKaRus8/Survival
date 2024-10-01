using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Logic.Interfaces.Providers
{
    public interface IAliveEnemyProvider
    {
        ObservableList<IEnemy> EnemiesRx { get; }
        IReadOnlyCollection<IEnemy> AliveEnemies { get; }
        IReadOnlyCollection<IEnemy> DeadEnemies { get; }
        int AliveEnemyCount { get; }
        void AddEnemy(IEnemy enemy);
        void RemoveEnemy(IEnemy enemy);
    }
}