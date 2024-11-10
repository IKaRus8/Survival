using System;
using JetBrains.Annotations;
using Logic.Interfaces.Providers;
using R3;

namespace Logic.Services.Enemy
{
    [UsedImplicitly]
    public class EnemyDeathObserver : IDisposable
    {
        private readonly IAliveEnemyProvider _aliveEnemyProvider;
        private readonly IDisposable _updateDisposable;

        public EnemyDeathObserver(IAliveEnemyProvider aliveEnemyProvider)
        {
            _aliveEnemyProvider = aliveEnemyProvider;          

            _updateDisposable = Observable.EveryUpdate().Subscribe(UpdateState);
        }

        private void UpdateState(Unit unit)
        {
            CheckDeadEnemy();
        }

        private void CheckDeadEnemy()
        {
            var enemies = _aliveEnemyProvider.AliveEnemies;

            foreach (var enemy in enemies)
            {
                if (enemy.CurrentHealth > 0)
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