using Data.Interfaces.Constants;

namespace Logic.Unity.Enemy.EnemyVariants
{
    public class EnemySimple : Enemy
    {
        public override string Id => Constants.Enemy.Id.SimpleEnemy;
    }
}