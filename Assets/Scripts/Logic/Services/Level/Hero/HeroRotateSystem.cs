using System;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroRotateSystem : IDisposable
    {
        private readonly IPlayerTargetObserver _targetProvider;
        private readonly CompositeDisposable _disposables;
        
        private IHero _hero;
        private IEnemy _target;

        public HeroRotateSystem(
            IHeroHolder heroHolder,
            IPlayerTargetObserver targetProvider)
        {
            _targetProvider = targetProvider;
            _disposables = new CompositeDisposable();
        
            heroHolder.HeroRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }    

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _hero = hero;

            _targetProvider.TargetRx.Subscribe(OnTargetChanged).AddTo(_disposables);
            Observable.EveryUpdate().Subscribe(RotateUpdate).AddTo(_disposables);
        }

        private void OnTargetChanged(IEnemy enemy)
        {
            _target = enemy;
        }

        private void RotateUpdate(Unit _)
        {
            if (_hero == null || _target == null)
            {
                return;
            }

            var direction = _target.Position - _hero.Transform.position;
        
            if ((_hero.Transform.forward - direction.normalized).sqrMagnitude > 0.1f)
            {
                _hero.Rotate(direction);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}