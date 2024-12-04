using System;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;
using Utilities.Extensions;

namespace Logic.Services.Level.Grid
{
    public class HeroDetectedService : IDisposable
    {
        private readonly IRectanglesProvider _rectanglesProvider;
        private readonly IGridSystem _gridSystem;
        private readonly IDisposable _heroDisposable;

        private IDisposable _updateDisposable;

        public ReactiveProperty<IGridElement> HeroGridElementRx { get; }

        public HeroDetectedService(
            IHeroHolder heroHolder,
            IRectanglesProvider rectanglesProvider,
            IGridSystem gridSystem)
        {
            _rectanglesProvider = rectanglesProvider;
            _gridSystem = gridSystem;
            HeroGridElementRx = new ReactiveProperty<IGridElement>();
            
            _heroDisposable = heroHolder.HeroRx.Subscribe(OnHeroCreated);
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _updateDisposable = Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(UpdateGrid);
        }

        private void UpdateGrid(Unit _)
        {
            var playerRectangle = _rectanglesProvider.GetHeroRectangle();
            
            foreach (var gridElement in _rectanglesProvider.GetGridRectangles())
            {
                var isPlayerInside = gridElement.IsIntersection(playerRectangle);

                if (!isPlayerInside)
                {
                    continue;
                }
                
                _gridSystem.ReplaceGridAround(gridElement.CenterPoint);
                
                break;
            }
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _updateDisposable?.Dispose();
            HeroGridElementRx?.Dispose();
        }
    }
}