namespace Logic.Interfaces
{
    public interface IDamageSystem 
    {
        void TakeDamage(IDamageble attacker, IDamageble victim, float damage);
    }
}
