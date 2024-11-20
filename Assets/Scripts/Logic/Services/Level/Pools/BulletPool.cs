using Logic.Unity.Weapon;
using Zenject;

namespace Logic.Services.Level.Pools
{
    public class BulletPool : MonoMemoryPool<Bullet>
    {
        protected override void OnDespawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}