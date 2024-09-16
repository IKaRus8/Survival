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
        public List<IEnemy> Enemies { get; }

        public ObservableList<IEnemy> EnemiesRx { get; }

        public IReadOnlyCollection<IEnemy> AliveEnemies => GetAliveEnemyes();
        public IReadOnlyCollection<IEnemy> DeadEnemies => GetDeadEnemyes();


        public int AliveEnemyCount => AliveEnemies.Count;


        public AliveEnemyProvider()
        {
            Enemies = new List<IEnemy>();
            EnemiesRx = new ObservableList<IEnemy>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            EnemiesRx.Add(enemy);
            Enemies.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            EnemiesRx.Remove(enemy);
            Enemies.Remove(enemy);
        }
        private List<IEnemy> GetAliveEnemyes()
        {
            var aliveEnemies = Enemies.Where(e => e.IsDead == false).ToList();

            return aliveEnemies;
        }

        private List<IEnemy> GetDeadEnemyes()
        {
            var deadEnemies = Enemies.Where(e => e.IsDead == true).ToList();

            return deadEnemies;
        }

    }
}
