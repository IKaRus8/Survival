using System.Collections.Generic;
using Logic.Unity.Projectiles;

namespace Logic.Interfaces.Providers.Level.Projectiles
{
    public interface IProjectilesProvider
    {
        IReadOnlyCollection<Projectile> ActiveProjectiles { get; }

        void AddProjectile(Projectile projectile);

        void RemoveProjectile(Projectile projectile);
    }
}