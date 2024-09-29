using Logic.Interfaces.Providers;
using R3;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Logic.Providers
{
    public class PlayerTargetsProvider : IPlayerTargetsProvider, IDisposable
    {
        private IAliveEnemyProvider _aliveEnemyProvider;        
        private IPlayerHolder _playerHolder;
        private CompositeDisposable _disposables=new CompositeDisposable();

        public ReactiveProperty<IEnemy> TargetRX { get; }

        public PlayerTargetsProvider(IAliveEnemyProvider aliveEnemyProvider, IPlayerHolder playerHolder)
        {
            _aliveEnemyProvider = aliveEnemyProvider;
            _playerHolder = playerHolder;
            TargetRX= new ReactiveProperty<IEnemy>();
            playerHolder.PlayerRx.Subscribe(InitTargetsObserv).AddTo(_disposables);
        }

        public void InitTargetsObserv(IPlayer player)
        {
            if (player == null)
            {
                return;
            }

            Observable.EveryUpdate().Subscribe(_ => FindNearestAliveTarget()).AddTo(_disposables);
        }

        public void FindNearestAliveTarget()
        {
            if (TargetRX.Value != null && !TargetRX.Value.IsDead) 
            { 
                return;
            }

            var enemies = _aliveEnemyProvider.AliveEnemies;
            var minDistance = float.MaxValue;
            var nearestEnemy = default(IEnemy);

            if (enemies == null || enemies.Count == 0)
            {
                return;
            }

            foreach (var enemy in enemies)
            {
                var distance = Vector3.SqrMagnitude(_playerHolder.PlayerRx.Value.Transform.position - enemy.Transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            TargetRX.Value = nearestEnemy;
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
       
  }
