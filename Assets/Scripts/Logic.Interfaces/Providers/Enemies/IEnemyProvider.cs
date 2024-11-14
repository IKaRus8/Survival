using System.Collections.Generic;

namespace Logic.Interfaces.Providers.Enemies
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