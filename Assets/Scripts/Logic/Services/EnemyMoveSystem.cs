using Logic.Interfaces.Providers;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using R3;

public class EnemyMoveSystem : IMovable, IDisposable
{
    private IAliveEnemyProvider _aliveEnemyProvider;
    private List<Enemy> _enemiesForMove;
    private ReactiveProperty<IPlayer> _player;

    private CompositeDisposable _disposables = new CompositeDisposable();

    public EnemyMoveSystem(IAliveEnemyProvider aliveEnemyProvider, IPlayerHolder playerHolder)
    {
        _aliveEnemyProvider = aliveEnemyProvider;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _enemiesForMove = (List<Enemy>)_aliveEnemyProvider.AliveEnemies;
            MoveUpdate();
        }).AddTo(_disposables);
        _player = playerHolder.PlayerRx;
    }

    private void MoveUpdate()
    {
        
        if (_enemiesForMove == null && _enemiesForMove.Count == 0)
        {
            return;
        }

        Debug.Log("MoveUpdate" + _enemiesForMove.Count + _aliveEnemyProvider.AliveEnemies.Count);

        foreach (var enemy in _enemiesForMove)
        {
            var dir = _player.Value.Transform.position+new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)) - enemy.transform.position;
            if (dir.sqrMagnitude > enemy.AttackDistance * enemy.AttackDistance)
            {
                var newdirection = dir.normalized * enemy.MoveSpeed*UnityEngine.Random.Range(0.2f, 1.2f) * Time.deltaTime;
                enemy.Move(newdirection);
            }
            else
            {
                enemy.Attack(_player.Value);
            }
        }
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
