using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Providers;
using UnityEditorInternal;
using UnityEngine;
using Zenject;

public class EnemySpawer: IEnemySpawner
{
    private readonly Enemy.Pool _enemyPool;
    private readonly IGridController _gridController;
    private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;

    private List<Enemy> aliveEnemyes = new();
    private float spawnProbability = 1f;

    public EnemySpawer(
        Enemy.Pool enemyPool, 
        IGridController gridController,
        IEnemySpawnSettingsProvider enemySpawnSettingsProvider)
    {
        _enemyPool = enemyPool;
        _gridController = gridController;
        _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
        StartSpawn();
    }
    
    private async void StartSpawn()
    {       
        await SpawnEnemyTimer();
    }

    private void AddEnemy(Enemy enemy)
    {
        aliveEnemyes.Add(enemy);
        enemy.OnDead += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        aliveEnemyes.Remove(enemy);
        _enemyPool.Despawn(enemy);
        enemy.OnDead -= RemoveEnemy;          
    }

    private float GetCurrentProbability(int countOfEnemyOnMap)
    {
        var enemyParameters = _enemySpawnSettingsProvider.GetAllParameters();

        foreach (var enemyParameter in enemyParameters.OrderBy(p => p.enemyQuantity))
        {
            if (countOfEnemyOnMap < enemyParameter.enemyQuantity)
            {
                return enemyParameter.spawnChance;
            }
        }
        
        return 0;
    }

    private void SpawnEnemy()
    {
        spawnProbability = GetCurrentProbability(aliveEnemyes.Count);
        
        var random = Random.Range(0f, 1f);
        
        if (spawnProbability >= random)
        {
            var enemy = _enemyPool.Spawn();
            
            enemy.transform.position = GetEnemyPos();
            
            AddEnemy(enemy);
        }
    }

    private Vector3 GetEnemyPos()
    {       
        var pointForSpawn = _gridController.GetRoadsForSpawn();
        Debug.Log(pointForSpawn.Count);
        return pointForSpawn[Random.Range(0, pointForSpawn.Count)].Transform.position;
    }

    private async UniTask SpawnEnemyTimer()
    {
        while (true)
        {
            await UniTask.Delay(1000);
            SpawnEnemy();
        }
    }    
}

public interface IEnemySpawner
{
    void AddEnemy(Enemy enemy) { }
    void RemoveEnemy(Enemy enemy) { }    
}
