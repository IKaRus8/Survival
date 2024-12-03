using System;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Interfaces.Services.Projectiles;
using Logic.RuntimeData.Rectangles;
using R3;
using Utilities.Extensions;

namespace Logic.Services.Level.Projectiles
{
    public class ProjectilesCollisionSystem : IProjectilesCollisionSystem, IDisposable
    {
        private readonly IRectanglesProvider _rectanglesProvider;
        private readonly IProjectileDamageSystem _damageSystem;
        private readonly IProjectilesProvider _projectilesProvider;
        private readonly IDisposable _updateDisposable;

        public ProjectilesCollisionSystem(
            IRectanglesProvider rectanglesProvider,
            IProjectileDamageSystem damageSystem,
            IProjectilesProvider projectilesProvider)
        {
            _rectanglesProvider = rectanglesProvider;
            _damageSystem = damageSystem;
            _projectilesProvider = projectilesProvider;
            
            _updateDisposable = Observable.EveryUpdate().Subscribe(Check);
        }

        private void Check(Unit _)
        {
            foreach (var projectile in _projectilesProvider.ActiveProjectiles)
            {
                var projectilesRectangle = new ProjectileRectangle(projectile);
                
                var nearestEnemies = _rectanglesProvider.GetNearestEnemyRectangles(projectilesRectangle);

                foreach (var enemyRectangle in nearestEnemies)
                {
                    if (projectilesRectangle.IsIntersection(enemyRectangle))
                    {
                        _projectilesProvider.RemoveProjectile(projectile);
                        
                        _damageSystem.DoDamage(projectile, enemyRectangle.EnemyLink);
                        
                        break;
                    }
                }
            }
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}