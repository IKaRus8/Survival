using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IDisposable
{
    private int startHealth = 100;
    private int currentHealth;

    private IMemoryPool _pool;

    public Action<Enemy> OnDead;

    public void Die()
    {
        OnDead?.Invoke(this);
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        currentHealth = startHealth;
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<Enemy>
    {

    }
}