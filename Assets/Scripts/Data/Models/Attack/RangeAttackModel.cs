namespace Data.Models.Attack
{
    public class RangeAttackModel : AttackModel
    {
        public float ProjectileSpeed { get; }

        public RangeAttackModel(
            string id, 
            float damage, 
            float attackSpeed, 
            float attackDistance,
            float projectileSpeed) 
            : base(id, damage, attackSpeed, attackDistance)
        {
            ProjectileSpeed = projectileSpeed;
        }
    }
}