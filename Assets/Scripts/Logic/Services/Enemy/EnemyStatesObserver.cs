using System;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Services.Enemy;
using Logic.Interfaces.Services.Player;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services.Enemy
{
    public class EnemyStatesObserver : IEnemyStatesObserver, IDisposable
    {
        private readonly IAliveEnemyProvider _aliveEnemyProvider;
        private readonly IDisposable _playerDisposable;
  
        private IDisposable _updateDisposable;
        private Transform _playerTransform;
        private List<EnemyStateModel> _enemyStates;

        public Subject<IReadOnlyCollection<EnemyStateModel>> EnemyStatesUpdated { get; }

        public EnemyStatesObserver(
            IAliveEnemyProvider aliveEnemyProvider, 
            IPlayerHolder playerHolder)
        {
            _aliveEnemyProvider = aliveEnemyProvider;
            EnemyStatesUpdated = new Subject<IReadOnlyCollection<EnemyStateModel>>();

            _playerDisposable = playerHolder.PlayerRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IPlayer player)
        {
            _playerTransform = player.Transform;
            
            _updateDisposable = Observable.EveryUpdate().Subscribe(EnemyUpdate);
        }

        private void EnemyUpdate(Unit _)
        {
            if (_playerTransform == null)
            {
                return;
            }

            _enemyStates.Clear();

            foreach (var enemy in _aliveEnemyProvider.AliveEnemies)
            {
                var enemyToPlayerVector = _playerTransform.position - enemy.Transform.position;

                var distance = enemyToPlayerVector.sqrMagnitude;

                _enemyStates.Add(new EnemyStateModel(enemy, enemyToPlayerVector, distance));
            }
            
            EnemyStatesUpdated.OnNext(_enemyStates);
        }

        public void Dispose()
        {
            _playerDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}