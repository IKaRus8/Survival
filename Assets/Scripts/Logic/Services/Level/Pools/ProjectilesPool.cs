using Logic.Unity.Projectiles;
using Zenject;

namespace Logic.Services.Level.Pools
{
    public class ProjectilesPool : MonoMemoryPool<Projectile>
    {
        protected override void OnSpawned(Projectile item)
        {
            item.Active();
        }

        protected override void OnDespawned(Projectile projectile)
        {
            projectile.Disable();
        }
    }
}