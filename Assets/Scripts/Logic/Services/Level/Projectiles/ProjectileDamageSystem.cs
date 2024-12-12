using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Attack;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Projectiles;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectileDamageSystem : IProjectileDamageSystem
    {
        private readonly IDamageSystem _damageSystem;

        public ProjectileDamageSystem(IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;
        }
        
        public void DoDamage(IProjectile projectile, IDamageable target)
        {
            _damageSystem.ToTarget(target).Do(projectile.Damage);
        }
    }
}