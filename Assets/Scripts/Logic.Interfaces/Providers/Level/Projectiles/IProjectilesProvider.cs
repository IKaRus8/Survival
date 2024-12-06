using System.Collections.Generic;
using Logic.Interfaces.Unity.Projectiles;
using Logic.Unity.Projectiles;

namespace Logic.Interfaces.Providers.Level.Projectiles
{
    public interface IProjectilesProvider
    {
        IReadOnlyCollection<IProjectile> ActiveProjectiles { get; }

        void RemoveProjectile(IProjectile projectile);
    }
}