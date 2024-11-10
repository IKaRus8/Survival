using System;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Interfaces.Services.Enemy;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services.Enemy
{
    public class EnemyMoveSystem : IMovable, IDisposable
    {
        private readonly IDisposable _updateDisposable;
  
        private IPlayer _player;

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
                var moveOffset = stateModel.Direction.normalized 
                                 * enemy.Model.MoveSpeed 
                                 * UnityEngine.Random.Range(0.2f, 1.2f) 
                                 * Time.deltaTime;
                
                enemy.Move(moveOffset);
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