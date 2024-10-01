using Logic.Interfaces.Providers;
using R3;
using System;
using Logic.Interfaces;
using UnityEngine;

namespace Logic.Providers
{
    public class PlayerTargetObserver : IPlayerTargetObserver, IDisposable
    {
        private readonly IAliveEnemyProvider _aliveEnemyProvider;
        private readonly IPlayerHolder _playerHolder;
        private readonly CompositeDisposable _disposables;

        public ReactiveProperty<IEnemy> TargetRx { get; }

        public PlayerTargetObserver(
            IAliveEnemyProvider aliveEnemyProvider,
            IPlayerHolder playerHolder)
        {
            _aliveEnemyProvider = aliveEnemyProvider;
            _playerHolder = playerHolder;
            
            TargetRx = new ReactiveProperty<IEnemy>();
            _disposables = new CompositeDisposable();
            
            playerHolder.PlayerRx.Subscribe(InitTargetsObserve).AddTo(_disposables);
        }

        private void InitTargetsObserve(IPlayer player)
        {
            if (player == null)
            {
                return;
            }

            Observable.EveryUpdate().Subscribe(_ => FindNearestAliveTarget()).AddTo(_disposables);
        }

        private void FindNearestAliveTarget()
        {
            if (TargetRx.Value != null && !TargetRx.Value.IsDead)
            {
                return;
            }

            var enemies = _aliveEnemyProvider.AliveEnemies;

            if (enemies == null || enemies.Count == 0)
            {
                return;
            }
            
            var minDistance = float.MaxValue;
            var nearestEnemy = default(IEnemy);

            foreach (var enemy in enemies)
            {
                var distance = Vector3.SqrMagnitude(_playerHolder.PlayerRx.Value.Transform.position - enemy.Transform.position);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            TargetRx.Value = nearestEnemy;
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}