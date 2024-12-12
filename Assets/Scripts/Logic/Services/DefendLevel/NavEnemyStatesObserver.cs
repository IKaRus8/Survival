using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level.Attack;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using Logic.Interfaces.Unity.Player;
using Logic.Services.Level.Enemy;
using UnityEngine;

namespace Logic.Services.DefendLevel
{
    public class NavEnemyStatesObserver : EnemyStatesObserver
    {
        private readonly IDefendObject _defendObject;
        private IDamageable _target;
        private IDamageable _hero;

        public NavEnemyStatesObserver(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder, 
            IDefendObject defendObject,
            IDamageSystem damageSystem) 
            : base(enemyProvider, heroHolder, damageSystem)
        {
            _defendObject = defendObject;
        }

        protected override void OnPlayerCreated(IHero hero)
        {
            base.OnPlayerCreated(hero);
            
            _hero = hero;
        }

        protected override Vector3 GetVectorTarget(Vector3 position)
        {
            var vectorToPlayer = base.GetVectorTarget(position);

            var vectorToDefend = _defendObject.Position - position;

            if (vectorToPlayer.sqrMagnitude > vectorToDefend.sqrMagnitude)
            {
                _target = _defendObject;
                
                return vectorToDefend;
            }

            _target = _hero;
            
            return vectorToPlayer;
        }

        protected override async UniTask TryAttack(IEnemy enemy)
        {
            if (!enemy.CanAttack)
            {
                return;
            }
            
            _damageSystem.ToTarget(_target).Do(enemy.EnemyAttackModel.Damage);
            
            await enemy.Attack();
        }
    }
}