using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Zenject;

public class EnemySpawer: IInitializable, IEnemySpawner
{
    private readonly Enemy.Pool _enemyPool;
    private readonly IRoadController _roadController;

    private List<Enemy> aliveEnemyes = new List<Enemy>();
    private float spawnProbability=100;

    public EnemySpawer(Enemy.Pool enemyPool, IRoadController roadController)
    {
        _enemyPool = enemyPool;
        _roadController = roadController;
    }

    public void Initialize()
    {
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
        float probability; 
        if(countOfEnemyOnMap < 10)
        {
            probability = 100;
        }
        else if(countOfEnemyOnMap >=10&&countOfEnemyOnMap < 20)
        {
            probability = 70;
        }
        else if(countOfEnemyOnMap >= 20 && countOfEnemyOnMap < 30)
        {
            probability = 30;
        }
        else
        {
            probability = 0;
        }
        return probability;
    }

    private void SpawnEnemy()
    {
        spawnProbability = GetCurrentProbability(aliveEnemyes.Count);
        float random = Random.Range(0, 101);
        if (spawnProbability >= random)
        {
            var enemy = _enemyPool.Spawn();
            enemy.transform.position = GetEnemyPos();
            AddEnemy(enemy);
        }
    }

    private Vector3 GetEnemyPos()
    {       
        var pointForSpawn = _roadController.GetRoadsForSpawn();
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
