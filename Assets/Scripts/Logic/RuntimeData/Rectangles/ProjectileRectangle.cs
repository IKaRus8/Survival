using Logic.Unity.Projectiles;

namespace Logic.RuntimeData.Rectangles
{
    public class ProjectileRectangle : Rectangle
    {
        private const float Radius = 0.2f;
        
        public Projectile ProjectileLink { get; }

        public ProjectileRectangle(Projectile projectile) : base(projectile.Position, Radius)
        {
            ProjectileLink = projectile;
        }
    }
}