using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Interfaces.Unity.Projectiles;
using Logic.Services.Level.Pools;
using Logic.Unity.Projectiles;

namespace Logic.Providers.Level.Projectiles
{
    public class ProjectilesProvider : IProjectilesProvider
    {
        private readonly ProjectilesPool _pool;

        public IReadOnlyCollection<IProjectile> ActiveProjectiles => GetActiveProjectiles();

        public ProjectilesProvider(ProjectilesPool pool)
        {
            _pool = pool;
        }

        public void RemoveProjectile(IProjectile projectile)
        {
            if (projectile.IsActive)
            {
                _pool.Despawn((Projectile)projectile);
            }
        }

        private IProjectile[] GetActiveProjectiles()
        {
            return _pool.Projectiles.Where(p => p.IsActive).ToArray();
        }
    }
}