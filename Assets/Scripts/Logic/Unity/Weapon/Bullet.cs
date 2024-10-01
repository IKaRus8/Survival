using System;
using DG.Tweening;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Weapon
{
    public class Bullet : MonoBehaviour, IPoolable<IPlayer, IEnemy, Transform, IDamageSystem, IMemoryPool>, IDisposable
    {
        private const float Damage = 50f;
        
        private IMemoryPool _pool;
        private Tween _tween;

        public void OnDespawned()
        {
            _tween?.Kill();
            _pool = null;
        }

        public void OnSpawned(
            IPlayer player,
            IEnemy target,
            Transform spawnPoint,
            IDamageSystem damageSystem,
            IMemoryPool pool)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            
            _pool = pool;
            
            _tween?.Kill();
            _tween = transform.DOMove(target.Transform.position, 0.15f).OnComplete(() => _pool.Despawn(this));
            
            damageSystem.DoDamage(player, target, Damage);
        }

        public void Dispose()
        {
            _tween?.Kill();
            
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<IPlayer, IEnemy, Transform, IDamageSystem, Bullet>
        { }
    }
}