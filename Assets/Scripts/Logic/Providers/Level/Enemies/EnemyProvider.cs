using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Unity.Enemy;

namespace Logic.Providers.Level.Enemies
{
    [UsedImplicitly]
    public class EnemyProvider : IEnemyProvider
    {
        private readonly List<IEnemy> _enemies;
        
        public IReadOnlyCollection<IEnemy> AliveEnemies => GetAliveEnemies().ToArray();
        public IReadOnlyCollection<IEnemy> DeadEnemies => GetDeadEnemies().ToArray();
        public int AliveEnemyCount => AliveEnemies.Count;
        
        public EnemyProvider()
        {
            _enemies = new List<IEnemy>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            _enemies.Add(enemy);
        }
        
        private IEnumerable<IEnemy> GetAliveEnemies()
        {
            var aliveEnemies = _enemies.Where(e => e.IsDead == false);

            return aliveEnemies;
        }

        private IEnumerable<IEnemy> GetDeadEnemies()
        {
            var deadEnemies = _enemies.Where(e => e.IsDead);

            return deadEnemies;
        }
    }
}