using System;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Interfaces.Services.Enemy;
using Logic.RuntimeData;
using R3;

namespace Logic.Services.Enemy
{
    public class EnemyAttackSystem : IDisposable
    {
        private readonly IDamageSystem _damageSystem;
        private readonly IDisposable _updateDisposable;

        private IPlayer _player;

        public EnemyAttackSystem(
            IEnemyStatesObserver enemyStatesObserver,
            IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;

            _updateDisposable = enemyStatesObserver.EnemyStatesUpdated.Subscribe(OnStatesUpdated);
        }

        private void TryAttack(EnemyStateModel stateModel)
        {
            var enemy = stateModel.Enemy;
            
            if (stateModel.Distance <= Math.Sqrt(enemy.Model.AttackDistance))
            {
                enemy.Attack(_player, _damageSystem);
            }
        }

        private void OnStatesUpdated(IReadOnlyCollection<EnemyStateModel> states)
        {
            foreach (var stateModel in states)
            {
                TryAttack(stateModel);
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}