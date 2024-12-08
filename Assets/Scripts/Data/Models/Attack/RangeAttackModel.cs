namespace Data.Models.Attack
{
    public class RangeAttackModel : AttackModel
    {
        public float ProjectileSpeed { get; }
        public string DestroyVfx { get; }

        public RangeAttackModel(
            string id, 
            float damage, 
            float attackSpeed, 
            float attackDistance,
            float projectileSpeed,
            string destroyVfx) 
            : base(id, damage, attackSpeed, attackDistance)
        {
            ProjectileSpeed = projectileSpeed;
            DestroyVfx = destroyVfx;
        }
    }
}