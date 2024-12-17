using System;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;
using UnityEngine;

namespace Logic.Services.Level
{
    public class CameraMovementSystem : IDisposable
    {
        private readonly Vector3 offset = new(0, 10, -5);
        private readonly Transform _transform;
        private readonly IDisposable _heroDisposable;
        
        private IDisposable _updateDisposable;
        private Transform _playerTransform;

        public CameraMovementSystem(
            IHeroHolder heroHolder,
            Camera camera)
        {
            _transform = camera.transform;

            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                _updateDisposable?.Dispose();
                
                return;
            }

            _playerTransform = hero.Transform;

            _updateDisposable = Observable.EveryUpdate().Subscribe(UpdateCameraPosition);
        }

        private void UpdateCameraPosition(Unit _)
        {
            _transform.position = _playerTransform.position + offset;
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}