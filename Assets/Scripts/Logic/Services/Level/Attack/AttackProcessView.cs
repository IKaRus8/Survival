using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Logic.Services.Level.Attack
{
    public class AttackProcessView
    {
        protected readonly TimeSpan _delay;
        
        private CancellationTokenSource _attackCancellationTokenSource;
        private UniTaskCompletionSource _currentAttackCompletionSource;

        public AttackProcessView(TimeSpan delay)
        {
            _delay = delay;
        }

        public virtual async UniTask<bool> Attack()
        {
            // Если атака уже выполняется
            if (_currentAttackCompletionSource != null)
            {
                return false;
                //await _currentAttackCompletionSource.Task;
            }

            // Подготавливаемся к новой атаке
            CancelAttack();
            
            _attackCancellationTokenSource = new CancellationTokenSource();
            _currentAttackCompletionSource = new UniTaskCompletionSource();

            bool result;

            try
            {
                // Выполняем подготовку атаки с учетом возможности отмены
                await AttackPrepare(_attackCancellationTokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                _currentAttackCompletionSource.TrySetException(ex);
                // Если атака была отменена
            }
            finally
            {
                // Завершаем текущую атаку
                result = _currentAttackCompletionSource.TrySetResult();
                _currentAttackCompletionSource = null;
            }
                
            return result;
        }

        public void CancelAttack()
        {
            if (_attackCancellationTokenSource != null && 
                !_attackCancellationTokenSource.IsCancellationRequested)
            {
                _attackCancellationTokenSource?.Cancel();
                _attackCancellationTokenSource?.Dispose();
            }
        }
        
        protected virtual async UniTask AttackPrepare(CancellationToken cancellationToken)
        {
            // Пример: Задержка для подготовки атаки
            await UniTask.Delay(_delay, cancellationToken: cancellationToken);
        }
    }
}