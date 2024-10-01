namespace Logic.Interfaces
{
    public interface IDamageSystem 
    {
        void DoDamage(IDamageble attacker, IDamageble target, float damage);
    }
}
