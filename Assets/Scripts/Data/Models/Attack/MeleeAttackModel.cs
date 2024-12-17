namespace Data.Models.Attack
{
    public class MeleeAttackModel : AttackModel
    {
        public MeleeAttackModel(
            string id, 
            float damage, 
            float attackSpeed,
            float attackDistance) 
            : base(id, damage, attackSpeed, attackDistance)
        {
        }
    }
}