using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject

public class EnemySpawer: IInitializable
{
    private Enemy.Factory enemyFactory;
    private List<Enemy> aliveEnemyes = new List<Enemy>();
    private float spawnProbability=0;

    public EnemySpawer(Enemy.Factory enemyFactory)
    {
        this.enemyFactory = enemyFactory;      
    }

    public void Initialize()
    {
        UniTask.Create(SpawnEnemyTimer);
    }

    private void AddEnemy(Enemy enemy)
    {
        aliveEnemyes.Add(enemy);
        enemy.OnDead += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        aliveEnemyes.Remove(enemy);
        enemy.OnDead -= RemoveEnemy;

        int countEnemysOnMap = aliveEnemyes.Count;
        spawnProbability = GetCurrentProbability(countEnemysOnMap);
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

    private void SpawnEnemy(float probability)
    {
        float random = Random.Range(0, 101);
        if (probability >= random)
        {
            var enemy = enemyFactory.Create();
            enemy.transform.position = GetEnemyPos();
            AddEnemy(enemy);
        }
    }

    private Vector3 GetEnemyPos()
    {       
        return new Vector3(0, 0, 0);
    }

    private UniTask SpawnEnemyTimer()
    {
        while (true)
        {
            UniTask.Delay(1000);
            SpawnEnemy(spawnProbability);
        }
    }

    
}

public interface IEnemySpawner
{    
}
