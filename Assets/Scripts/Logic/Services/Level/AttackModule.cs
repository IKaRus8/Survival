using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Logic.Services.Level
{
    public class AttackModule
    {
        private readonly float _damage;
        private readonly TimeSpan _delay;
        
        private CancellationTokenSource _attackCancellationTokenSource;
        private UniTaskCompletionSource _currentAttackCompletionSource;

        public AttackModule(float damage, TimeSpan delay)
        {
            _damage = damage;
            _delay = delay;
        }

        public virtual async UniTask<float> Attack()
        {
            // Если атака уже выполняется
            if (_currentAttackCompletionSource != null)
            {
                return 0f;
                //await _currentAttackCompletionSource.Task;
            }

            // Подготавливаемся к новой атаке
            CancelAttack();
            
            _attackCancellationTokenSource = new CancellationTokenSource();
            _currentAttackCompletionSource = new UniTaskCompletionSource();

            try
            {
                // Выполняем подготовку атаки с учетом возможности отмены
                await AttackPrepare(_attackCancellationTokenSource.Token);

                return _damage;
            }
            catch (OperationCanceledException)
            {
                // Если атака была отменена
                return 0f;
            }
            finally
            {
                // Завершаем текущую атаку
                _currentAttackCompletionSource.TrySetResult();
                _currentAttackCompletionSource = null;
            }
        }

        public void CancelAttack()
        {
            _attackCancellationTokenSource?.Cancel();
            _attackCancellationTokenSource?.Dispose();
        }
        
        private async UniTask AttackPrepare(CancellationToken cancellationToken)
        {
            // Пример: Задержка для подготовки атаки
            await UniTask.Delay(_delay, cancellationToken: cancellationToken);
        }
    }
}