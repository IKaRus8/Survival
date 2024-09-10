using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Logic.Interfaces.Providers
{

    public interface IAliveEnemyProvider
    {
        List<Enemy> Enemies { get; }
        ObservableList<Enemy> EnemiesRx { get; }
        IReadOnlyCollection<Enemy> AliveEnemies { get; }
        IReadOnlyCollection<Enemy> DeadEnemies { get; }
        int AliveEnemyCount { get; }
        void AddEnemy(Enemy enemy);
        void RemoveEnemy(Enemy enemy);
    }

}