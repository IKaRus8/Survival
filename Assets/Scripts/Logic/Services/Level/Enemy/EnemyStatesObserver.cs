using System;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Providers.Enemies;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Services.Player;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemyStatesObserver : IEnemyStatesObserver, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _playerDisposable;
  
        private IDisposable _updateDisposable;
        private Transform _playerTransform;
        private List<EnemyStateModel> _enemyStates;

        public Subject<IReadOnlyCollection<EnemyStateModel>> EnemyStatesUpdated { get; }

        public EnemyStatesObserver(
            IEnemyProvider enemyProvider, 
            IPlayerHolder playerHolder)
        {
            _enemyProvider = enemyProvider;
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

            foreach (var enemy in _enemyProvider.AliveEnemies)
            {
                var enemyToPlayerVector = _playerTransform.position - enemy.EnemyTransform.position;

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