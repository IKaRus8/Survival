using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Unity.Enemy.EnemyVariants
{
    public class EnemyNav : Enemy
    {
        [SerializeField, Required]
        private NavMeshAgent _agent;
        
        public override string Id => Constants.Enemy.Id.NavEnemy;

        public override void Move(Vector3 targetPosition)
        {
            _agent.isStopped = false;
            
            _agent.destination = targetPosition;
        }

        public override UniTask AttackPrepare()
        {
            _agent.isStopped = true;
            
            return base.AttackPrepare();
        }

        public override UniTask Die()
        {
            _agent.isStopped = true;
            
            return base.Die();
        }

        public override void ReInitialize()
        {
            base.ReInitialize();
            
            _agent.isStopped = false;
        }
    }
}