using Logic.Unity.Weapon;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Pools
{
    public class BulletPool : MonoMemoryPool<Vector3, Bullet>
    {
        protected override void Reinitialize(Vector3 position, Bullet bullet)
        {
            bullet.Initialize(position);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}