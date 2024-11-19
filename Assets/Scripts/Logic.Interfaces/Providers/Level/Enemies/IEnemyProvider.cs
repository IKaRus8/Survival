using System.Collections.Generic;
using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Providers.Level.Enemies
{
    public interface IEnemyProvider
    {
        IReadOnlyCollection<IEnemy> AliveEnemies { get; }
        IReadOnlyCollection<IEnemy> DeadEnemies { get; }
        int AliveEnemyCount { get; }
        void AddEnemy(IEnemy enemy);
        void RemoveEnemy(IEnemy enemy);
    }
}