using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using Logic.Interfaces.Providers;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : IDisposable
{   
    private readonly IGridController _gridController;
    private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
    private readonly List<Enemy> _aliveEnemies;
    private readonly CompositeDisposable _disposables;

    private IAssetService _assetService;
    private IInstantiator _container;

    private GameObject _enemyPrefab;
    private bool _isGetPrefab=false;
    
    private List<EnemySpawnSettings.SpawnParameter> _enemyParameters;

    public EnemySpawner(
        IGridController gridController,
        IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
        IAssetService assetServise,
        IInstantiator diContainer)
    {
        _gridController = gridController;
        _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
        _aliveEnemies = new();
        _disposables = new CompositeDisposable();
        _enemySpawnSettingsProvider.IsSettingLoadedRx.Subscribe(StartSpawn).AddTo(_disposables);
        _assetService = assetServise;
        _container = diContainer;
        Init();
    }

    private void Init()
    {
        GetPrefabAsync();
    }

    private async void GetPrefabAsync()
    {
        _enemyPrefab = await _assetService.GetAssetAsync<GameObject>("Enemy");
        _isGetPrefab = true;
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
        if(_isGetPrefab == false) 
        { 
            return; 
        }
        var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn(_aliveEnemies.Count);
        
        var random = Random.Range(0f, 1f);

        if (random > spawnProbability)
        {
            return;
        }
        
        var enemy = _container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab);
            
        enemy.transform.position = GetEnemyPos();
            
        AddEnemy(enemy);
    }

    private Vector3 GetEnemyPos()
    {
        var pointForSpawn = _gridController.GetRoadsForSpawn();

        Debug.Log(pointForSpawn.Count);

        var gridElementCollider = pointForSpawn[Random.Range(0, pointForSpawn.Count)].Collider;

        return GetRandomPositionWithinField(gridElementCollider);
    }

    private Vector3 GetRandomPositionWithinField(Collider gameField)
    {       
        Vector3 minBounds = gameField.bounds.min;
        Vector3 maxBounds = gameField.bounds.max;
        
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}
