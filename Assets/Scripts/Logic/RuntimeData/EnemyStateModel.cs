using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using UnityEngine;

namespace Logic.RuntimeData
{
    public class EnemyStateModel
    {
        public IEnemy Enemy { get; }
        public Vector3 Direction { get; }
        public float Distance { get; }

        public EnemyStateModel(IEnemy enemy, Vector3 direction, float distance)
        {
            Distance = distance;
            Direction = direction;
            Enemy = enemy;
        }
    }
}