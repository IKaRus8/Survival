using System;
using Logic.Interfaces;
using R3;
using UnityEngine;

namespace Logic.Services
{
    public class CameraMovementSystem : IDisposable
    {
        private readonly Vector3 offset = new(0, 10, -5);
        private readonly Transform _transform;
        private readonly CompositeDisposable _disposables;
    
        private Transform _playerTransform;

        public CameraMovementSystem(
            IPlayerHolder playerHolder,
            Camera camera)
        {
            _transform = camera.transform;
            _disposables = new CompositeDisposable();

            playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }

        private void UpdateCameraPosition(Unit _)
        {
            if (_playerTransform == null)
            {
                return;
            }

            _transform.position = _playerTransform.position + offset;
        }

        private void OnPlayerCreated(IPlayer player)
        {
            if (player == null)
            {
                return;
            }

            _playerTransform = player.Transform;

            Observable.EveryUpdate().Subscribe(UpdateCameraPosition).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}