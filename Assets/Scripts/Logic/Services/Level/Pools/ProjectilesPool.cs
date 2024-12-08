using System.Collections.Generic;
using Data.Models.Attack;
using Logic.Interfaces.Unity.Projectiles;
using Logic.Unity.Projectiles;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Pools
{
    public class ProjectilesPool : MonoMemoryPool<RangeAttackModel, Vector3, Vector3, Projectile>
    {
        public HashSet<IProjectile> Projectiles { get; } = new();
        
        protected override void OnCreated(Projectile item)
        {
            item.gameObject.SetActive(false);
            
            Projectiles.Add(item);
        }

        protected override void OnSpawned(Projectile item)
        {
            item.Active();
        }

        protected override void OnDespawned(Projectile item)
        {
            item.Disable();
        }

        protected override void Reinitialize(
            RangeAttackModel p1,
            Vector3 p2,
            Vector3 p3,
            Projectile item)
        {
            item.Initialization(p1, p2, p3);
        }
    }
}