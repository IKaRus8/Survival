using Logic.Interfaces.Unity;
using UnityEngine;

namespace Logic.RuntimeData
{
    public class EnemyRectangle : Rectangle
    {
        public IEnemy EnemyLink { get; private set; }
        
        public EnemyRectangle(IEnemy enemy, Vector3[] positions) : base(positions)
        {
            EnemyLink = enemy;
        }

        public EnemyRectangle(IEnemy enemy, Vector3 point, float radius) : base(point, radius)
        {
            EnemyLink = enemy;
        }
    }
}