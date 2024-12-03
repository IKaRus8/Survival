using Logic.Interfaces.Unity.Enemy;
using UnityEngine;

namespace Logic.RuntimeData.Rectangles
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