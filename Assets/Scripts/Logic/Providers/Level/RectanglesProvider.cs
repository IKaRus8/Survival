using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using Logic.RuntimeData.Rectangles;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Providers.Level
{
    public class RectanglesProvider : IRectanglesProvider, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _heroDisposable;

        private Transform _heroTransform;

        public RectanglesProvider(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder)
        {
            _enemyProvider = enemyProvider;

            _heroDisposable = heroHolder.HeroRx.Subscribe(OnHeroCreated);
        }

        public HashSet<EnemyRectangle> GetEnemyRectangles()
        {
            return _enemyProvider.AliveEnemies.Select(e => new EnemyRectangle(e, e.Position)).ToHashSet();
        }

        public Rectangle GetHeroRectangle()
        {
            if (_heroTransform == null)
            {
                return new Rectangle(Vector3.zero, 0f);
            }

            return new Rectangle(_heroTransform.position, 0.8f);
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

        public virtual HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle)
        {
            var result = new HashSet<EnemyRectangle>();

            var gridRectangles = GetGridRectangleBy(rectangle);

            foreach (var enemyRectangle in GetEnemyInRectangle(gridRectangles))
            {
                result.Add(enemyRectangle);
            }

            return result;
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }

            _heroTransform = hero.Transform;
        }

        public virtual Rectangle GetGridRectangleBy(Rectangle rectangle)
        {
            return new Rectangle(Vector3.zero, 300f);
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
        }
    }
}