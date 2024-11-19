using System;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Level.Grid
{
    public class PlayerDetectedService : IDisposable
    {
        private readonly IGridSystem _gridSystem;
        private readonly IDisposable _heroDisposable;

        private Transform _playerTransform;
        private IDisposable _updateDisposable;

        public ReactiveProperty<IGridElement> PlayerGridElementRx { get; }

        public PlayerDetectedService(
            IHeroHolder heroHolder,
            IGridSystem gridSystem)
        {
            _gridSystem = gridSystem;
            PlayerGridElementRx = new ReactiveProperty<IGridElement>();
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _playerTransform = hero.Transform;

            _updateDisposable = Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(UpdateGrid);
        }

        private void UpdateGrid(Unit _)
        {
            var playerRectangle = new Rectangle(_playerTransform.position, 1f);
            
            DebugRectangleDrawer.Clear();
            DebugRectangleDrawer.AddRectangle(playerRectangle);
            
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

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
            PlayerGridElementRx?.Dispose();
        }
    }
}