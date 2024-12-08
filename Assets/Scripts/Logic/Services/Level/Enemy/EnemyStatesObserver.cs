using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemyStatesObserver : IEnemyStatesObserver, IPauseHandler, IDisposable
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDamageSystem _damageSystem;
        private readonly IDisposable _playerDisposable;
  
        private IDisposable _updateDisposable;
        private Transform _playerTransform;

        public EnemyStatesObserver(
            IEnemyProvider enemyProvider, 
            IHeroHolder heroHolder,
            IDamageSystem damageSystem)
        {
            _enemyProvider = enemyProvider;
            _damageSystem = damageSystem;

            _playerDisposable = heroHolder.HeroRx.Subscribe(OnPlayerCreated);

            Resume();
        }

        public void Pause()
        {
            _updateDisposable?.Dispose();
        }

        public void Resume()
        {
            _updateDisposable = Observable.EveryUpdate().Subscribe(EnemyUpdate);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _playerTransform = hero.Transform;
            
            
        }

        private void EnemyUpdate(Unit _)
        {
            foreach (var enemy in _enemyProvider.AliveEnemies)
            {
                var enemyToPlayerVector = _playerTransform.position - enemy.Position;

                var sqrDistance = enemyToPlayerVector.sqrMagnitude;

                var needMove = sqrDistance > enemy.EnemyAttackModel.SqrAttackDistance;
                
                if (needMove)
                {
                    MoveEnemy(enemy, enemyToPlayerVector.normalized);
                }
                else
                {
                    TryAttack(enemy).Forget();
                }
            }
        }

        private void MoveEnemy(IEnemy enemy, Vector3 direction)
        {
            var moveOffset = direction 
                             * enemy.Model.MoveSpeed 
                             * Time.deltaTime;

            enemy.Rotate(direction);
            enemy.Move(RandomHelper.GetRandomizedVector(moveOffset, 0.2f));
        }
        
        private async UniTask TryAttack(IEnemy enemy)
        {
            if (!enemy.CanAttack)
            {
                return;
            }
            
            _damageSystem.ToHero().Do(enemy.EnemyAttackModel.Damage);
            
            await enemy.Attack();
        }

        public void Dispose()
        {
            _playerDisposable?.Dispose();
            _updateDisposable?.Dispose();
        }
    }
}