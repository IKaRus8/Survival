
namespace Logic.Interfaces
{
    public interface IDamageble
    {
        float Armor { get; }   
        float DamageResistance { get; }
        float DamageReflection  { get; }
        float Vampirism { get; }
    
        void TakeDamage(IDamageble attacker, float damage);
        void Heal(float healAmount);
    }
}