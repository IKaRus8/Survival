using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Providers.Level
{
    public class RectanglesProvider : IRectanglesProvider, IDisposable
    {
        private const float MobSize = 1f;
        
        private readonly IGridSystem _gridSystem;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _heroDisposable;
        
        private Transform _heroTransform;

        public RectanglesProvider(
            IGridSystem gridSystem,
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder)
        {
            _gridSystem = gridSystem;
            _enemyProvider = enemyProvider;

            _heroDisposable = heroHolder.HeroRx.Subscribe(OnHeroCreated);
        }
        
        public IReadOnlyCollection<EnemyRectangle> GetEnemyRectangles()
        {
            return _enemyProvider.AliveEnemies.Select(e => new EnemyRectangle(e, e.Position, MobSize)).ToArray();
        }

        public IReadOnlyCollection<GridRectangle> GetGridRectangles()
        {
            return _gridSystem.Grid.Select(g => g.ElementRectangle).ToArray();
        }

        public GridRectangle GetGridRectangle(int index)
        {
            return _gridSystem[index].ElementRectangle;
        }

        public Rectangle GetHeroRectangle()
        {
            if (_heroTransform == null)
            {
                return new Rectangle(Vector3.zero, 0f);
            }
            
            return new Rectangle(_heroTransform.position, MobSize);
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _heroTransform = hero.Transform;
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
        }
    }
}