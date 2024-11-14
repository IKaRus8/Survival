using Logic.Unity.Weapon;
using UnityEngine;
using Zenject;

namespace Logic.Services.Pools
{
    public class BulletPool : MonoMemoryPool<Vector3, Vector3, Bullet>
    {
        protected override void Reinitialize(Vector3 position, Vector3 direction, Bullet bullet)
        {
            bullet.Initialize(position, direction);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}