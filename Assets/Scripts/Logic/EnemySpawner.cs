using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Logic.Interfaces.Providers;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : IDisposable
{
    private readonly Enemy.Pool _enemyPool;
    private readonly IGridController _gridController;
    private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
    private readonly List<Enemy> _aliveEnemies;
    private readonly CompositeDisposable _disposables;
    
    private List<EnemySpawnSettings.SpawnParameter> _enemyParameters;

    public EnemySpawner(
        Enemy.Pool enemyPool, 
        IGridController gridController,
        IEnemySpawnSettingsProvider enemySpawnSettingsProvider)
    {
        _enemyPool = enemyPool;
        _gridController = gridController;
        _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
        _aliveEnemies = new();
        _disposables = new CompositeDisposable();

        _enemySpawnSettingsProvider.IsSettingLoadedRx.Subscribe(StartSpawn).AddTo(_disposables);
    }

    private void AddEnemy(Enemy enemy)
    {
        _aliveEnemies.Add(enemy);
        
        //TODO: Observe system
        enemy.OnDead += RemoveEnemy;
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _aliveEnemies.Remove(enemy);
        _enemyPool.Despawn(enemy);
        
        enemy.OnDead -= RemoveEnemy;          
    }
    
    private void StartSpawn(bool value)
    {
        if (!value)
        {
            return;
        }
        
        Observable.Interval(TimeSpan.FromSeconds(1f))
            .Subscribe(_ => SpawnEnemy())
            .AddTo(_disposables);
    }

    private void SpawnEnemy()
    {
        var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn(_aliveEnemies.Count);
        
        var random = Random.Range(0f, 1f);

        if (random > spawnProbability)
        {
            return;
        }
        
        var enemy = _enemyPool.Spawn();
            
        enemy.transform.position = GetEnemyPos();
            
        AddEnemy(enemy);
    }

    private Vector3 GetEnemyPos()
    {       
        var pointForSpawn = _gridController.GetRoadsForSpawn();
        
        Debug.Log(pointForSpawn.Count);
        
        return pointForSpawn[Random.Range(0, pointForSpawn.Count)].Transform.position;
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}
