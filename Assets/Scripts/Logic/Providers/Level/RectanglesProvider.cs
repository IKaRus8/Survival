using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.RuntimeData.Rectangles;
using R3;
using UnityEngine;
using Utilities.Extensions;

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
        
        public HashSet<EnemyRectangle> GetEnemyRectangles()
        {
            return _enemyProvider.AliveEnemies.Select(e => new EnemyRectangle(e, e.Position, MobSize)).ToHashSet();
        }

        public HashSet<Rectangle> GetGridRectangles()
        {
            return _gridSystem.Grid.Select(g => g.ElementRectangle).ToHashSet();
        }

        public Rectangle GetHeroRectangle()
        {
            if (_heroTransform == null)
            {
                return new Rectangle(Vector3.zero, 0f);
            }
            
            return new Rectangle(_heroTransform.position, MobSize);
        }

        public HashSet<EnemyRectangle> GetEnemyInRectangle(Rectangle rectangle)
        {
            var enemiesInGrid = new HashSet<EnemyRectangle>();

            // Проверяем врагов на пересечение с текущим прямоугольником
            foreach (var enemy in GetEnemyRectangles())
            {
                if (rectangle.IsIntersection(enemy))
                {
                    enemiesInGrid.Add(enemy);
                }
            }
            
            return enemiesInGrid;
        }

        public async IAsyncEnumerable<EnemyRectangle[]> GetEnemiesByGridElements(CancellationToken cancellationToken)
        {
            var grid = GetGridRectangles();

            if (grid.IsNullOrEmpty())
            {
                yield return Array.Empty<EnemyRectangle>();
            }
            
            foreach (var gridRectangle in grid)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                
                yield return GetEnemyInRectangle(gridRectangle).ToArray();

                await UniTask.Yield();
            }
        }

        public HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle)
        {
            var result = new HashSet<EnemyRectangle>();
            
            var gridRectangles = GetGridRectangleBy(rectangle);

            foreach (var enemyRectangle in GetEnemyInRectangle(gridRectangles))
            {
                result.Add(enemyRectangle);
            }

            return result;
        }

        public Rectangle GetGridRectangleBy(Rectangle rectangle)
        {
            foreach (var gridRectangle in GetGridRectangles())
            {
                if (rectangle.IsIntersection(gridRectangle))
                {
                    return gridRectangle;
                }
            }

            return new Rectangle(Vector3.zero, 0f);
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