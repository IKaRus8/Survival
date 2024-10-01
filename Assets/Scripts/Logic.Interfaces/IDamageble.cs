
namespace Logic.Interfaces
{
    public interface IDamageble
    {
        void TakeDamage(IDamageble attacker, float damage);
        void Heal(float healAmount);
    }
}