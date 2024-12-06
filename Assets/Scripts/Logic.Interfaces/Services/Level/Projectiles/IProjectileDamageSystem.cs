using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Projectiles;

namespace Logic.Interfaces.Services.Level.Projectiles
{
    public interface IProjectileDamageSystem
    {
        void DoDamage(IProjectile projectile, IDamageable target);
    }
}