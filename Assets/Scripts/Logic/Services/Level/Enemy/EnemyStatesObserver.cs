using System;
using System.Collections.Generic;
using Logic.Interfaces;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemyStatesObserver : IEnemyStatesObserver, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _playerDisposable;
        private readonly List<EnemyStateModel> _enemyStates;
  
        private IDisposable _updateDisposable;
        private Transform _playerTransform;

        public Subject<IReadOnlyCollection<EnemyStateModel>> EnemyStatesUpdated { get; }

        public EnemyStatesObserver(
            IEnemyProvider enemyProvider, 
            IHeroHolder heroHolder)
        {
            _enemyProvider = enemyProvider;
            EnemyStatesUpdated = new Subject<IReadOnlyCollection<EnemyStateModel>>();
            _enemyStates = new List<EnemyStateModel>();

            //_playerDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _playerTransform = hero.Transform;
            
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
                var enemyToPlayerVector = _playerTransform.position - enemy.Position;

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