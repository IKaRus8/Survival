using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.RuntimeData.Rectangles;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Providers.Level
{
    public class SurvivalLevelRectanglesProvider : RectanglesProvider, ISurvivalLevelRectanglesProvider
    {
        private readonly IGridSystem _gridSystem;
        
        public SurvivalLevelRectanglesProvider(
            IGridSystem gridSystem,
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder) 
            : base(enemyProvider, heroHolder)
        {
            _gridSystem = gridSystem;
        }

        public HashSet<Rectangle> GetGridRectangles()
        {
            return _gridSystem.Grid.Select(g => g.ElementRectangle).ToHashSet();
        }

        public async IAsyncEnumerable<EnemyRectangle[]> GetEnemiesByGridElements(
            [EnumeratorCancellation] CancellationToken cancellationToken)
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

        public override Rectangle GetGridRectangleBy(Rectangle rectangle)
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

        public override HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle)
        {
            var result = new HashSet<EnemyRectangle>();

            var gridRectangles = GetGridRectangleBy(rectangle);

            foreach (var enemyRectangle in GetEnemyInRectangle(gridRectangles))
            {
                result.Add(enemyRectangle);
            }

            return result;
        }
    }
}