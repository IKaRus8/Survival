using System;
using R3;
using Logic.Providers;
using UnityEngine;

public class PlayerRotateSystem : IRotatable, IDisposable
{
    private IPlayer _player;   
    private ReactiveProperty<Enemy> _targetRX;

    private readonly CompositeDisposable _disposables;

    public PlayerRotateSystem(
            IPlayerHolder playerHolder,
            IPlayerTargetsProvider targetProvider)
    {

        _disposables = new CompositeDisposable();
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);

        _targetRX = targetProvider.TargetRX;
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

    public void RotateUpdate()
    {
        if (_player.Transform == null || _targetRX.Value == null)
        {
            return;
        }

        var direction = _targetRX.Value.transform.position - _player.Transform.position;
        if ((_player.Transform.forward - direction.normalized).sqrMagnitude > 0.1f)
        {
            _player.IsAiming.Value = true;
            _player.Rotate(direction, _player.Speed, Time.deltaTime);
        }
        else
        {
            _player.IsAiming.Value = false;
        }
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}
