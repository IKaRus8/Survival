using Logic.Interfaces;
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