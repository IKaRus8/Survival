using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Logic.Interfaces.Providers;
using UnityEngine.Rendering;

namespace Logic.Providers
{
    [UsedImplicitly]
    public class AliveEnemyProvider : IAliveEnemyProvider
    {
        public List<Enemy> Enemies { get; }
        
        public ObservableList<Enemy> EnemiesRx { get; }

        public IReadOnlyCollection<Enemy> AliveEnemies => GetAliveEnemies();
        
        public int AliveEnemyCount => AliveEnemies.Count;

        public AliveEnemyProvider()
        {
            Enemies = new List<Enemy>();
            EnemiesRx = new ObservableList<Enemy>();
        }
        
        public void AddEnemy(Enemy enemy)
        {
            EnemiesRx.Add(enemy);
            Enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            EnemiesRx.Remove(enemy);
            Enemies.Remove(enemy);
        }

        private List<Enemy> GetAliveEnemies()
        {
            var aliveEnemies = Enemies.Where(e => e.IsDead == false).ToList();
            
            return aliveEnemies;
        }
    }
}