using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Services.Level.Pools;
using Logic.Unity.Projectiles;

namespace Logic.Providers.Level.Projectiles
{
    public class ProjectilesProvider : IProjectilesProvider
    {
        private readonly ProjectilesPool _pool;
        private readonly HashSet<Projectile> _projectiles;

        public IReadOnlyCollection<Projectile> ActiveProjectiles => GetActiveProjectiles();

        public ProjectilesProvider(ProjectilesPool pool)
        {
            _pool = pool;
            _projectiles = new HashSet<Projectile>();
        }
        
        public void AddProjectile(Projectile projectile)
        {
            _projectiles.Add(projectile);
        }

        public void RemoveProjectile(Projectile projectile)
        {
            if (projectile.IsActive)
            {
                _pool.Despawn(projectile);
            }
        }

        private IReadOnlyCollection<Projectile> GetActiveProjectiles()
        {
            return _projectiles.Where(p => p.IsActive).ToArray();
        }
    }
}