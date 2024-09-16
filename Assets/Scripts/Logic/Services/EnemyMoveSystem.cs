using Logic.Interfaces.Providers;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using R3;

public class EnemyMoveSystem : IMovable, IDisposable
{
    private readonly IAliveEnemyProvider _aliveEnemyProvider;
    private readonly CompositeDisposable _disposables;
  
    private IPlayer _player;

    public EnemyMoveSystem(IAliveEnemyProvider aliveEnemyProvider, IPlayerHolder playerHolder)
    {
        _aliveEnemyProvider = aliveEnemyProvider;
        _disposables = new CompositeDisposable();

        Observable.EveryUpdate().Subscribe(MoveUpdate).AddTo(_disposables);
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
    }

    private void MoveUpdate(Unit _)
    {
        if (_player == null)
        {
            return;
        }

        foreach (var enemy in _aliveEnemyProvider.AliveEnemies)
        {
            var direction = _player.Transform.position +
                      new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)) -
                      enemy.Transform.position;

            var distance = direction.sqrMagnitude;
            
            if (distance > enemy.AttackDistance * enemy.AttackDistance)
            {
                var newdirection = direction.normalized * enemy.MoveSpeed * UnityEngine.Random.Range(0.2f, 1.2f) *
                                   Time.deltaTime;
                
                enemy.Move(newdirection);
            }                      
        }
    }

    private void OnPlayerCreated(IPlayer player)
    {
        _player = player;
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}