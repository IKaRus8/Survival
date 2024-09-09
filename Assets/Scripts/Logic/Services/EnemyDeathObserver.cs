using JetBrains.Annotations;
using Logic.Interfaces.Providers;
using R3;

namespace Logic.Services
{
    [UsedImplicitly]
    public class EnemyDeathObserver
    {
        private readonly IAliveEnemyProvider _aliveEnemyProvider;

        public EnemyDeathObserver(IAliveEnemyProvider aliveEnemyProvider)
        {
            _aliveEnemyProvider = aliveEnemyProvider;

            Observable.EveryUpdate().Subscribe(_ => UpdateState());
        }

        private void UpdateState()
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
    }
}