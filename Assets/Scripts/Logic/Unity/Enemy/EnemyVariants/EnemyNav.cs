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
            _agent.destination = targetPosition;
        }
    }
}