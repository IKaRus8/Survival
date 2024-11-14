using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Services.Player;
using Logic.Services.Pools;
using R3;
using UnityEngine;

namespace Logic.Services.Player
{
    public class PlayerShootService : IDisposable
    {
        private readonly TimeSpan _shotDelay = TimeSpan.FromSeconds(1f);
        
        private readonly ReactiveProperty<IEnemy> _targetRx;
        private readonly CompositeDisposable _disposables;
        private readonly IDamageSystem _damageSystem;
        private BulletPool _bulletPool;

        private IPlayer _player;
        private Transform _shotPoint;

        public bool IsCanShoot { get; private set; }

        public PlayerShootService(
            IPlayerHolder playerHolder,
            IPlayerTargetObserver targetProvider,
            
            IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;
            _disposables = new CompositeDisposable();
            
            _targetRx = targetProvider.TargetRx;
            IsCanShoot = true;
            
            playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
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
            // слишком много условий, нужно переработать
            if (_player == null
                || _targetRx.Value == null
                || _targetRx.Value.IsDead
                || _player.IsRotating.Value
                || _shotPoint == null)
            {
                return;
            }

            Shot();
        }

        public void Shot()
        {
            if (!IsCanShoot)
            {
                return;
            }
            
            ShotDelayTimer().Forget();

            _bulletPool.Spawn(_player.Transform.position, _player.Transform.forward);
        }

        private async UniTaskVoid ShotDelayTimer()
        {
            IsCanShoot = false;
            
            await UniTask.Delay(_shotDelay);
            
            IsCanShoot = true;
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}