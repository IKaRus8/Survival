using System;
using JetBrains.Annotations;
using Logic.Interfaces.Providers.Level.Enemies;
using R3;

namespace Logic.Services.Level.Enemy
{
    [UsedImplicitly]
    public class EnemyDeathObserver : IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _updateDisposable;

        public EnemyDeathObserver(IEnemyProvider enemyProvider)
        {
            _enemyProvider = enemyProvider;          

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
                
                enemy.Die();              
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}