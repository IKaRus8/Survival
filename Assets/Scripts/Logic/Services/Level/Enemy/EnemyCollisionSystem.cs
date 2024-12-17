using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Utilities.Extensions;

namespace Logic.Services.Level.Enemy
{
    public class EnemyCollisionSystem : IDisposable
    {
        private const float Offset = 0.2f;

        private readonly ISurvivalLevelRectanglesProvider _rectanglesProvider;
        private readonly IDisposable _updateDisposable;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public EnemyCollisionSystem(ISurvivalLevelRectanglesProvider rectanglesProvider)
        {
            _rectanglesProvider = rectanglesProvider;
            _cancellationTokenSource = new CancellationTokenSource();

            Update().Forget();
        }

        private async UniTaskVoid Update()
        {
            var token = _cancellationTokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                await CheckCollisions(token);
            }
        }

        private async UniTask CheckCollisions(CancellationToken cancellationToken)
        {
            await foreach (var enemiesInGrid in 
                           _rectanglesProvider.GetEnemiesByGridElements(cancellationToken))
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }
                
                // Проверяем пересечения между врагами внутри текущей зоны
                for (var i = 0; i < enemiesInGrid.Length; i++)
                {
                    var enemy = enemiesInGrid[i];
                    for (var j = i + 1; j < enemiesInGrid.Length; j++)
                    {
                        var anotherEnemy = enemiesInGrid[j];

                        if (enemy.IsIntersection(anotherEnemy))
                        {
                            var reverseVector = enemy.EnemyLink.Position - anotherEnemy.EnemyLink.Position;
                            var direction = reverseVector.normalized * Offset;

                            // Двигаем врага, избегая пересечения
                            enemy.EnemyLink.Move(RandomHelper.GetRandomizedVector(direction, 0.3f));

                            break; // Прерываем, чтобы не обрабатывать одного врага несколько раз
                        }
                    }
                }
            }

            await UniTask.Yield();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _updateDisposable?.Dispose();
        }
    }
}