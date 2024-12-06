using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Unity.Enemy;

namespace Logic.Services.Level.Attack
{
    public class EnemyAttackProcessView : AttackProcessView
    {
        private readonly EnemyViewController _viewController;

        public EnemyAttackProcessView(TimeSpan delay, EnemyViewController viewController) 
            : base(delay)
        {
            _viewController = viewController;
        }

        protected override async UniTask AttackPrepare(CancellationToken cancellationToken)
        {
            await _viewController.PostAttack((float) _delay.TotalSeconds, cancellationToken);
        }
    }
}