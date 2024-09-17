using JetBrains.Annotations;
using Logic.Interfaces.Presenters;
using Logic.Providers;
using Logic.Interfaces.Providers;
using R3;

namespace Logic.Services
{
    [UsedImplicitly]
    public class EnemyDeathObserver
    {
        private readonly IAliveEnemyProvider _aliveEnemyProvider;
        private readonly IGameEndedPopupPresenter _gameEndedPopupPresenter;

        public EnemyDeathObserver(
            IAliveEnemyProvider aliveEnemyProvider,
            IGameEndedPopupPresenter gameEndedPopupPresenter)
        {
            _aliveEnemyProvider = aliveEnemyProvider;
            _gameEndedPopupPresenter = gameEndedPopupPresenter;

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
                _gameEndedPopupPresenter.ShowPopup();
            }
        }
    }
}