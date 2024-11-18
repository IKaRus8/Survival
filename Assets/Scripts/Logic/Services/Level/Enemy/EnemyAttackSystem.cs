using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using R3;

namespace Logic.Services.Level.Enemy
{
    public class EnemyAttackSystem : IDisposable
    {
        private readonly IDamageSystem _damageSystem;
        private readonly IDisposable _updateDisposable;

        private IHero _hero;

        public EnemyAttackSystem(
            IEnemyStatesObserver enemyStatesObserver,
            IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;

            _updateDisposable = enemyStatesObserver.EnemyStatesUpdated.Subscribe(OnStatesUpdated);
        }

        private void OnStatesUpdated(IReadOnlyCollection<EnemyStateModel> states)
        {
            foreach (var stateModel in states)
            {
                TryAttack(stateModel).Forget();
            }
        }

        private async UniTask TryAttack(EnemyStateModel stateModel)
        {
            var enemy = stateModel.Enemy;
            
            if (stateModel.Distance <= Math.Sqrt(enemy.Model.AttackDistance))
            {
                var damage = await enemy.Attack();

                if (damage <= 0)
                {
                    return;
                }
                
                _damageSystem.ToHero().Do(damage);
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}