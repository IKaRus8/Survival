using System;
using Logic.Interfaces;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;

namespace Logic.Services.Level
{
    public class CameraMovementSystem : IDisposable
    {
        private readonly Vector3 offset = new(0, 10, -5);
        private readonly Transform _transform;
        private readonly CompositeDisposable _disposables;
    
        private Transform _playerTransform;

        public CameraMovementSystem(
            IHeroHolder heroHolder,
            Camera camera)
        {
            _transform = camera.transform;
            _disposables = new CompositeDisposable();

            heroHolder.HeroRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }

        private void UpdateCameraPosition(Unit _)
        {
            if (_playerTransform == null)
            {
                return;
            }

            _transform.position = _playerTransform.position + offset;
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _playerTransform = hero.Transform;

            Observable.EveryUpdate().Subscribe(UpdateCameraPosition).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}