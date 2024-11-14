using Logic.Interfaces;

namespace Logic.Services.Level
{
    public class DamageSystem : IDamageSystem
    {
        public void DoDamage(IDamageable attacker, IDamageable target, float damage)
        {
            
        }

        public IDamageSystem FromPlayer()
        {


            return this;
        }

        public IDamageSystem FromEnemy()
        {
            
            
            return this;
        }

        public IDamageSystem ToEnemy()
        {
            
            
            return this;
        }

        public IDamageSystem ToPlayer()
        {
            
            
            return this;
        }
    }
}