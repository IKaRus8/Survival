using Logic.Interfaces.Unity;
using Logic.Unity.Projectiles;

namespace Logic.Interfaces.Services.Projectiles
{
    public interface IProjectileDamageSystem
    {
        void DoDamage(Projectile projectile, IDamageable target);
    }
}