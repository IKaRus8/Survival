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
    public class DefendEnemyStatesObserver : EnemyStatesObserver
    {
        private const float HeroAttackSqrDistance = 70f;
        
        private readonly IDamageable _defendObject;

        public DefendEnemyStatesObserver(
            IEnemyProvider enemyProvider,
            IHeroHolder heroHolder, 
            IDefendObject defendObject,
            IDamageSystem damageSystem) 
            : base(enemyProvider, heroHolder, damageSystem)
        {
            _defendObject = defendObject;
        }

        protected override TargetModel GetTargetModel(Vector3 position, IDamageable target)
        {
            var heroTargetModel = base.GetTargetModel(position, target);

            if (heroTargetModel.Distance < HeroAttackSqrDistance)
            {
                return heroTargetModel;
            }
            
            return base.GetTargetModel(position, _defendObject);
        }

        protected override void MoveEnemy(IEnemy enemy, TargetModel targetModel)
        {
            enemy.Rotate(targetModel.Direction);
            
            enemy.Move(targetModel.Target.Position);
        }
    }
}