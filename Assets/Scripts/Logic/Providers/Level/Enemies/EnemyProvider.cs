using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Unity;

namespace Logic.Providers.Level.Enemies
{
    [UsedImplicitly]
    public class EnemyProvider : IEnemyProvider
    {
        private List<IEnemy> _enemies;
        
        public IReadOnlyCollection<IEnemy> AliveEnemies => GetAliveEnemies();
        public IReadOnlyCollection<IEnemy> DeadEnemies => GetDeadEnemies();
        public int AliveEnemyCount => AliveEnemies.Count;
        
        public EnemyProvider()
        {
            _enemies = new List<IEnemy>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }
        
        private List<IEnemy> GetAliveEnemies()
        {
            var aliveEnemies = _enemies.Where(e => e.IsDead == false).ToList();

            return aliveEnemies;
        }

        private List<IEnemy> GetDeadEnemies()
        {
            var deadEnemies = _enemies.Where(e => e.IsDead).ToList();

            return deadEnemies;
        }
    }
}