using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logic.Interfaces;
using Logic.Interfaces.Providers;

namespace Logic.Providers
{
    [UsedImplicitly]
    public class AliveEnemyProvider : IAliveEnemyProvider
    {
        private List<IEnemy> Enemies { get; }
        public IReadOnlyCollection<IEnemy> AliveEnemies => GetAliveEnemies();
        public IReadOnlyCollection<IEnemy> DeadEnemies => GetDeadEnemies();
        public int AliveEnemyCount => AliveEnemies.Count;
        
        public AliveEnemyProvider()
        {
            Enemies = new List<IEnemy>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            Enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            Enemies.Remove(enemy);
        }
        
        private List<IEnemy> GetAliveEnemies()
        {
            var aliveEnemies = Enemies.Where(e => e.IsDead == false).ToList();

            return aliveEnemies;
        }

        private List<IEnemy> GetDeadEnemies()
        {
            var deadEnemies = Enemies.Where(e => e.IsDead).ToList();

            return deadEnemies;
        }
    }
}