using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers.Level;
using Logic.RuntimeData;
using R3;
using Utilities.Extensions;

namespace Logic.Services.Level.Enemy
{
    public class EnemyCollisionSystem : IDisposable
    {
        private const float Offset = 0.05f;
        
        private readonly IRectanglesProvider _rectanglesProvider;
        private readonly IDisposable _updateDisposable;

        public EnemyCollisionSystem(
            IRectanglesProvider rectanglesProvider)
        {
            _rectanglesProvider = rectanglesProvider;

            _updateDisposable = Observable.EveryUpdate().Subscribe(Check);
        }

        private void Check(Unit _)
        {
            var grid = _rectanglesProvider.GetGridRectangles();
            var enemies = _rectanglesProvider.GetEnemyRectangles().ToList();

            foreach (var gridRectangle in grid)
            {
                var enemiesInRectangle = new List<EnemyRectangle>();
                
                foreach (var enemy in enemies.ToList())
                {
                    if (gridRectangle.IsIntersection(enemy))
                    {
                        enemies.Remove(enemy);
                        
                        enemiesInRectangle.Add(enemy);
                    }
                }

                foreach (var enemy in enemiesInRectangle.ToList())
                {
                    enemiesInRectangle.Remove(enemy);

                    foreach (var anotherEnemy in enemiesInRectangle)
                    {
                        if (enemy.IsIntersection(anotherEnemy))
                        {
                            var reverseVector = enemy.EnemyLink.Position - anotherEnemy.EnemyLink.Position;
                            
                            var direction = reverseVector.normalized * Offset;
                            
                            enemy.EnemyLink.Move( RandomHelper.GetRandomizedVector(direction, 0.3f));
                            
                            break;
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}