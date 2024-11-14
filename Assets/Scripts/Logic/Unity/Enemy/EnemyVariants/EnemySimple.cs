using Data.Interfaces.Models;

namespace Logic.Unity.Enemy.EnemyVariants
{
    public class EnemySimple : Enemy
    {
        public override IEnemyModel Model { get; protected set; }
    }
}