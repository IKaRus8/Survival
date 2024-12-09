using Logic.Interfaces.Unity.Enemy;
using UnityEngine;

namespace Logic.RuntimeData.Rectangles
{
    public class EnemyRectangle : Rectangle
    {
        private const float Radius = 0.1f;
        
        public IEnemy EnemyLink { get; private set; }

        public EnemyRectangle(IEnemy enemy, Vector3 point) : base(point, Radius)
        {
            EnemyLink = enemy;
        }
    }
}