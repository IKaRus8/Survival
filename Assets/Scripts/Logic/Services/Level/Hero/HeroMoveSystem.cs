using System;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroMoveSystem : IDisposable
    {
        private readonly IInput _input;
        private readonly IDisposable _heroDisposable;
        
        private IHero _hero;
        private IDisposable _updateDisposable;
        
        public HeroMoveSystem(
            IHeroHolder heroHolder,
            IInput input)
        {
            _input = input;
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                _updateDisposable?.Dispose();
                    
                return;
            }
            
            _hero = hero;
            
            _updateDisposable = Observable.EveryUpdate().Subscribe(MoveUpdate);
        }

        private void MoveUpdate(Unit _)
        {
            var direction = _input.Direction;
            
            _hero.Move(direction);      
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}