using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Attack;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using Logic.Interfaces.Unity.Player;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemyStatesObserver : IEnemyStatesObserver, IPauseHandler, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        protected readonly IDamageSystem _damageSystem;
        private readonly IDisposable _playerDisposable;
  
        private IDisposable _updateDisposable;
        protected IDamageable _hero;

        public EnemyStatesObserver(
            IEnemyProvider enemyProvider, 
            IHeroHolder heroHolder,
            IDamageSystem damageSystem)
        {
            _enemyProvider = enemyProvider;
            _damageSystem = damageSystem;

            _playerDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);
        }

        public void Pause()
        {
            _updateDisposable?.Dispose();
        }

        public void Resume()
        {
            _updateDisposable = Observable.EveryUpdate().Subscribe(EnemyUpdate);
        }

        protected virtual void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                Pause();
                
                return;
            }
            
            _hero = hero;

            Resume();
        }

        private void EnemyUpdate(Unit _)
        {
            foreach (var enemy in _enemyProvider.AliveEnemies)
            {
                var targetModel = GetTargetModel(enemy.Position, _hero);

                var needMove = targetModel.Distance > enemy.EnemyAttackModel.SqrAttackDistance;
                
                if (needMove)
                {
                    MoveEnemy(enemy, targetModel);
                }
                else
                {
                    TryAttack(enemy, targetModel.Target).Forget();
                }
            }
        }

        protected virtual void MoveEnemy(IEnemy enemy, TargetModel targetModel)
        {
            if (enemy.IsAttack)
            {
                return;
            }
            
            var moveOffset = targetModel.Direction 
                             * enemy.Model.MoveSpeed 
                             * Time.deltaTime;

            enemy.Rotate(targetModel.Direction);
            enemy.Move(RandomHelper.GetRandomizedVector(moveOffset, 0.2f));
        }
        
        protected virtual async UniTask TryAttack(IEnemy enemy, IDamageable target)
        {
            if (enemy.IsAttack)
            {
                return;
            }

            await enemy.AttackPrepare();
            
            _damageSystem.ToTarget(target).Do(enemy.EnemyAttackModel.Damage);
            
            enemy.Attack().Forget();
        }

        protected virtual TargetModel GetTargetModel(Vector3 position, IDamageable target)
        {
            var enemyToTargetVector = target.Position - position;
            var sqrDistance = enemyToTargetVector.sqrMagnitude;
            var direction = enemyToTargetVector.normalized;

            return new TargetModel(target, direction, sqrDistance);
        }

        public virtual void Dispose()
        {
            _playerDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
        
        protected class TargetModel
        {
            public IDamageable Target { get; }
            public Vector3 Direction { get; }
            public float Distance { get; }

            public TargetModel(
                IDamageable target,
                Vector3 direction,
                float distance)
            {
                Target = target;
                Direction = direction;
                Distance = distance;
            }
        }
    }
}