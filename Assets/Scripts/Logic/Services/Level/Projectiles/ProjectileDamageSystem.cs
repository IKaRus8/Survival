using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Projectiles;
using Logic.Interfaces.Unity;
using Logic.Unity.Projectiles;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectileDamageSystem : IProjectileDamageSystem
    {
        private readonly IDamageSystem _damageSystem;

        public ProjectileDamageSystem(IDamageSystem damageSystem)
        {
            _damageSystem = damageSystem;
        }
        
        public void DoDamage(Projectile projectile, IDamageable target)
        {
            _damageSystem.ToTarget(target).Do(projectile.ProjectileDamage.Value);
        }
    }
}