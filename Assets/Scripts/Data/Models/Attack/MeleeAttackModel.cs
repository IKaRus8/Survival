namespace Data.Models.Attack
{
    public class MeleeAttackModel : AttackModel
    {
        private const float AttackDistance = 1.5f;
        
        public MeleeAttackModel(
            string id, 
            float damage, 
            float attackSpeed) 
            : base(id, damage, attackSpeed, AttackDistance)
        {
        }
    }
}