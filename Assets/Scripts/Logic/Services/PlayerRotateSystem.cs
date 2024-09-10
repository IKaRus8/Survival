using System;
using R3;
using Logic.Providers;
using UnityEngine;

public class PlayerRotateSystem : IRotatable, IDisposable
{
    private IPlayer _player;   
    private Enemy _target;

    private readonly CompositeDisposable _disposables;

    public PlayerRotateSystem(
            IPlayerHolder playerHolder,
            IPlayerTargetsProvider targetProvider)
    {

        _disposables = new CompositeDisposable();
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);

        targetProvider.TargetRX.Subscribe(OnTargetChanged).AddTo(_disposables);
    }    

    private void OnPlayerCreated(IPlayer player)
    {
        if (player == null)
        {
            return;
        }

        _player = player;

        Observable.EveryUpdate().Subscribe(_ => RotateUpdate()).AddTo(_disposables);
    }

    private void OnTargetChanged(Enemy enemy)
    {
        _target = enemy;
    }

    public void RotateUpdate()
    {
        if (_player.Transform == null || _target == null)
        {
            return;
        }

        var direction = _target.transform.position - _player.Transform.position;
        
        if ((_player.Transform.forward - direction.normalized).sqrMagnitude > 0.1f)
        {
            _player.IsRotating.Value = true;
            _player.Rotate(direction, _player.Speed, Time.deltaTime);
        }
        else
        {
            _player.IsRotating.Value = false;
        }
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}
