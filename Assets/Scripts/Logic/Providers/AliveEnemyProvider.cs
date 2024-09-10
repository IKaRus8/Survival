using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using Logic.Interfaces.Providers;

namespace Logic.Providers
{
    [UsedImplicitly]
    public class AliveEnemyProvider : IAliveEnemyProvider
    {
        public List<Enemy> Enemies { get; }

        public ObservableList<Enemy> EnemiesRx { get; }

        public IReadOnlyCollection<Enemy> AliveEnemies => GetAliveEnemyes();
        public IReadOnlyCollection<Enemy> DeadEnemies => GetDeadEnemyes();


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
        private List<Enemy> GetAliveEnemyes()
        {
            var aliveEnemies = Enemies.Where(e => e.IsDead == false).ToList();

            return aliveEnemies;
        }

        private List<Enemy> GetDeadEnemyes()
        {
            var deadEnemies = Enemies.Where(e => e.IsDead == true).ToList();

            return deadEnemies;
        }

    }
}
