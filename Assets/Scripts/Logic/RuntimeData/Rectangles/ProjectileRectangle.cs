using Logic.Interfaces.Unity.Projectiles;

namespace Logic.RuntimeData.Rectangles
{
    public class ProjectileRectangle : Rectangle
    {
        private const float Radius = 0.2f;
        
        public IProjectile ProjectileLink { get; }

        public ProjectileRectangle(IProjectile projectile) : base(projectile.Position, Radius)
        {
            ProjectileLink = projectile;
        }
    }
}