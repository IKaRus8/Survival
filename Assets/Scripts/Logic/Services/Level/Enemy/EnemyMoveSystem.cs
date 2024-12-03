using System;
using System.Collections.Generic;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemyMoveSystem : IDisposable
    {
        private readonly IDisposable _updateDisposable;
  
        private IHero _hero;

        public EnemyMoveSystem(IEnemyStatesObserver enemyStatesObserver)
        {
            _updateDisposable = enemyStatesObserver.EnemyStatesUpdated.Subscribe(OnStatesUpdated);
        }

        private void MoveEnemy(EnemyStateModel stateModel)
        {
            var enemy = stateModel.Enemy;
            
            var needMove = NeedEnemyMove(enemy, stateModel.Distance);
            
            if (needMove)
            {
                var moveOffset = stateModel.Direction 
                                 * enemy.Model.MoveSpeed 
                                 * Time.deltaTime;
                
                enemy.Move(RandomHelper.GetRandomizedVector(moveOffset, 0.2f));
            }
        }

        private bool NeedEnemyMove(IEnemy enemy, float targetDistance)
        {
            return targetDistance > Math.Sqrt(enemy.Model.AttackDistance);
        }
        
        private void OnStatesUpdated(IReadOnlyCollection<EnemyStateModel> states)
        {
            foreach (var stateModel in states)
            {
                MoveEnemy(stateModel);
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}