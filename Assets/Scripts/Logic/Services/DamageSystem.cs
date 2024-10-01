using Logic.Interfaces;

namespace Logic.Services
{
    public class DamageSystem : IDamageSystem
    {
        public void TakeDamage(IDamageble attacker, IDamageble victim, float damage)
        {
            var resultDamage = CalculateDamage(victim, damage);
            victim.TakeDamage(attacker, CalculateDamage(victim, damage));
            CalculateVampirism(attacker, resultDamage);
            attacker.Heal(CalculateVampirism(attacker, resultDamage));
            attacker.TakeDamage(victim, CalculateReflection(victim, damage));
        }

        public float CalculateDamage(IDamageble victim, float damage)
        {
            return damage - victim.DamageResistance - victim.Armor;                
        }

        public float CalculateVampirism(IDamageble attacker, float damage)
        { 
            return (damage*attacker.Vampirism)/100;
        }

        public float CalculateReflection(IDamageble victim, float damage)
        {
            return (victim.DamageReflection*damage)/100;
        }
    }
}
