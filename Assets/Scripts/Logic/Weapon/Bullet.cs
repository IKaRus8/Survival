using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable<Enemy, Transform, IMemoryPool>, IDisposable
{
    private IMemoryPool _pool;
    private float damage=50f;
    private Tween _tween;

    public void Dispose()
    {
        _tween?.Kill();
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _tween?.Kill();
        _pool = null;        
    }

    public void OnSpawned(Enemy target, Transform SpawnPoint, IMemoryPool pool)
    {
        transform.position = SpawnPoint.position;
        transform.rotation = SpawnPoint.rotation;
        _pool = pool;
        _tween?.Kill();
        _tween = transform.DOMove(target.transform.position, 0.15f).OnComplete(() => _pool.Despawn(this));
        target.TakeDamage(damage);
    }

    public class Factory : PlaceholderFactory<Enemy, Transform, Bullet>
    {
    }
}
