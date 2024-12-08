using System;
using Logic.Interfaces;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Hero
{
    public class HeroMoveSystem : IDisposable
    {
        private readonly IInput _input;
        private readonly CompositeDisposable _disposables;
        
        private IHero _hero;
        
        public HeroMoveSystem(
            IHeroHolder heroHolder,
            IInput input)
        {
            _input = input;
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
            
            Observable.EveryUpdate().Subscribe(MoveUpdate).AddTo(_disposables);
        }

        private void MoveUpdate(Unit _)
        {
            var direction = _input.Direction;
            
            _hero.Move(direction);      
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}