using R3;
using UnityEditor.ShaderGraph.Configuration;
using UnityEngine;
using Logic.Providers;
using System;
using Cysharp.Threading.Tasks;

public class PlayerWeapon : IWeapon, IDisposable
{
    private IPlayer _player;
    private ReactiveProperty<IEnemy> _targetRx;
    private  CompositeDisposable _disposables = new CompositeDisposable();
    private float _shotDelay=1f;

    private Bullet.Factory _bulletFactory;

    public bool IsCanShoot{ get; private set; }
    public float ShotDelay => _shotDelay; 

    private Transform _shotPoint;
    public PlayerWeapon(IPlayerHolder playerHolder, IPlayerTargetsProvider targetProvider, Bullet.Factory bulletFactory)
    {
        _bulletFactory = bulletFactory;
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        _targetRx = targetProvider.TargetRX;   
        
        IsCanShoot = true;
    }

    private void OnPlayerCreated(IPlayer player)
    {
        if (player == null)
        {
            return;
        }

        _player = player;
        _shotPoint = player.WeaponShootPoint;
        Observable.EveryUpdate().Subscribe(_ => TryFire()).AddTo(_disposables);
    }  

    public void TryFire()
    {
        if (_player == null || _targetRx.Value == null || _targetRx.Value.IsDead||_player.IsRotating.Value||_shotPoint==null) return;

        Shot();
    }

    public void Shot()
    {
        if(!IsCanShoot) return;       
        ShotDelayTimer().Forget();
        _bulletFactory.Create(_targetRx.Value, _shotPoint);
    }

    private async UniTaskVoid ShotDelayTimer()
    {
        IsCanShoot = false;
        await UniTask.Delay((int)ShotDelay * 1000);
        IsCanShoot = true;
    }
    

    public void Dispose()
    {
       _disposables.Dispose();
    }
}
