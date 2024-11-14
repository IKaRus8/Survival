namespace Logic.Interfaces
{
    public interface IDamageSystem 
    {
        void DoDamage(IDamageable attacker, IDamageable target, float damage);
        
        IDamageSystem FromPlayer();

        IDamageSystem FromEnemy();

        IDamageSystem ToEnemy();

        IDamageSystem ToPlayer();
    }
}
