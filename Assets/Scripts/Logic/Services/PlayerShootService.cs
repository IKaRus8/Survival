using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using R3;
using UnityEngine;

namespace Logic.Weapon
{
    public class PlayerShootService : IDisposable
    {
        private readonly TimeSpan _shotDelay = TimeSpan.FromSeconds(1f);
        
        private readonly ReactiveProperty<IEnemy> _targetRx;
        private readonly CompositeDisposable _disposables;
        private readonly IDamageSystem _damageSystem;
        private readonly Bullet.Factory _bulletFactory;

        private IPlayer _player;
        private Transform _shotPoint;

        public bool IsCanShoot { get; private set; }

        public PlayerShootService(
            IPlayerHolder playerHolder,
            IPlayerTargetObserver targetProvider,
            Bullet.Factory bulletFactory,
            IDamageSystem damageSystem)
        {
            _bulletFactory = bulletFactory;
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
            
            _bulletFactory.Create(_player, _targetRx.Value, _shotPoint, _damageSystem);
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