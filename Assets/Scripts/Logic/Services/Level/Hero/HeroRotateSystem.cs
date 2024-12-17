using System;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroRotateSystem : IDisposable
    {
        private readonly IInput _input;
        private readonly IPlayerTargetObserver _targetProvider;
        private readonly IDisposable _heroDisposables;
        
        private CompositeDisposable _updateDisposables;
        private IHero _hero;
        private IEnemy _target;

        public HeroRotateSystem(
            IHeroHolder heroHolder,
            IInput input,
            IPlayerTargetObserver targetProvider)
        {
            _input = input;
            _targetProvider = targetProvider;
            _updateDisposables = new CompositeDisposable();
        
            _heroDisposables = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }    

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                _updateDisposables?.Dispose();
                _updateDisposables = new CompositeDisposable();
                
                return;
            }

            _hero = hero;

            _targetProvider.TargetRx.Subscribe(OnTargetChanged).AddTo(_updateDisposables);
            Observable.EveryUpdate().Subscribe(RotateUpdate).AddTo(_updateDisposables);
        }

        private void OnTargetChanged(IEnemy enemy)
        {
            _target = enemy;
        }

        private void RotateUpdate(Unit _)
        {
            if (_target == null || _target.IsDead)
            {
                _hero.Rotate(_input.Direction);
                
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
            _updateDisposables?.Dispose();
            _heroDisposables?.Dispose();
        }
    }
}