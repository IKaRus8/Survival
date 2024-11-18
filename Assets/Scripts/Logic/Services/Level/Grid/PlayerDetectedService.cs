using System;
using Logic.Interfaces;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Level.Grid
{
    public class PlayerDetectedService
    {
        private readonly IGridSystem _gridSystem;
        private Transform _playerTransform;
        
        public ReactiveProperty<IGridElement> PlayerGridElementRx { get; }

        public PlayerDetectedService(
            IHeroHolder heroHolder,
            IGridSystem gridSystem)
        {
            _gridSystem = gridSystem;
            PlayerGridElementRx = new ReactiveProperty<IGridElement>();
            
            heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            _playerTransform = hero.Transform;

            Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(UpdateGrid);
        }

        private void UpdateGrid(Unit _)
        {
            var playerRectangle = new Rectangle(_playerTransform.position, 5f);

            foreach (var gridElement in _gridSystem.Grid)
            {
                var isPlayerInside = gridElement.ElementRectangle.IsIntersection(playerRectangle);

                if (!isPlayerInside)
                {
                    continue;
                }
                
                _gridSystem.ReplaceGridAround(gridElement.Index);
                
                break;
            }
        }
    }
}