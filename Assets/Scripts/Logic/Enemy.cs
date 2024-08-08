using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolable<IMemoryPool>
{

    public Action <Enemy>OnDead;
    private int startHealth=100;
    private int currentHealth;

    private IMemoryPool _pool;
    private IAssetService _assetService;



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
       currentHealth = startHealth;       
    }

    public class Factory : PlaceholderFactory<Enemy>
    {
    }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, Enemy>
    {
    }    


    public static async  void InitPool(DiContainer container, IAssetService assetService)
    {
        var enemyPrefab = assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Enemy.prefab");
       
        container
            .BindFactory<Enemy, Factory>()
            .FromPoolableMemoryPool<Enemy, Pool>(poolBinder =>
                poolBinder
                    .WithInitialSize(30)
                    .FromComponentInNewPrefab(enemyPrefab)); //// как тут быть
    }

    public void OnSpawned(IAssetService assetService, IMemoryPool pool)
    {
        _assetService = assetService;
        _pool = pool;
    }
}
