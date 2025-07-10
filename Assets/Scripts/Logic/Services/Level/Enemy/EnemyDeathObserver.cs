using System;
using JetBrains.Annotations;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity.Enemy;
using R3;

namespace Logic.Services.Level.Enemy
{
    [UsedImplicitly]
    public class EnemyDeathObserver : IEnemyDeathObserver, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _updateDisposable;
        
        public Subject<IEnemy> EnemyDeadRx { get; }

        public EnemyDeathObserver(IEnemyProvider enemyProvider)
        {
            _enemyProvider = enemyProvider;    
            EnemyDeadRx = new Subject<IEnemy>();

            _updateDisposable = Observable.EveryUpdate().Subscribe(UpdateState);
        }

        private void UpdateState(Unit unit)
        {
            FindDeadEnemy();
        }

        private void FindDeadEnemy()
        {
            var enemies = _enemyProvider.AliveEnemies;

            foreach (var enemy in enemies)
            {
                if (enemy.Health > 0)
                {
                    continue;
                }
                
                KillEnemy(enemy);
            }
        }

        private void KillEnemy(IEnemy enemy)
        {
            enemy.Die();
            
            EnemyDeadRx.OnNext(enemy);
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}