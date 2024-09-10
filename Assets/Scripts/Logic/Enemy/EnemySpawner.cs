using System;
using System.Collections.Generic;
using Data.ScriptableObjects;
using R3;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Logic.Interfaces.Providers;


public class EnemySpawner : IDisposable
{
    private const string EnemyKey = "Enemy";

    private readonly IGridController _gridController;
    private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
    private readonly CompositeDisposable _disposables;
    private readonly IAssetService _assetService;
    private readonly IInstantiator _container;
    private readonly IAliveEnemyProvider _aliveEnemyProvider;

    private GameObject _enemyPrefab;
    private bool _isGetPrefab;

    private List<EnemySpawnSettings.SpawnParameter> _enemyParameters;

    public EnemySpawner(
        IGridController gridController,
        IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
        IAssetService assetService,
        IInstantiator diContainer,
        IAliveEnemyProvider aliveEnemyProvider)
    {
        _gridController = gridController;
        _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
        _assetService = assetService;
        _container = diContainer;
        _aliveEnemyProvider = aliveEnemyProvider;

        _disposables = new CompositeDisposable();

        _enemySpawnSettingsProvider.IsSettingLoadedRx.Subscribe(StartSpawn).AddTo(_disposables);

        Init();
    }

    private void Init()
    {
        GetPrefabAsync();
    }

    private async void GetPrefabAsync()
    {
        _enemyPrefab = await _assetService.GetAssetAsync<GameObject>(EnemyKey);
        _isGetPrefab = true;
    }

    private void AddEnemy(Enemy enemy)
    {
        _aliveEnemyProvider.AddEnemy(enemy);
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
        if (_isGetPrefab == false)
        {
            return;
        }

        var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn();

        var random = Random.Range(0f, 1f);

        if (random > spawnProbability)
        {
            return;
        }

        
        if (_aliveEnemyProvider.DeadEnemies.Count == 0)
        {
            var enemy = _container.InstantiatePrefabForComponent<Enemy>(_enemyPrefab);
            PrepareEnemy(enemy);
        }
        else
        {
            var  enemyList = (List<Enemy>)_aliveEnemyProvider.DeadEnemies;
            var enemy = enemyList[0];
            PrepareEnemy(enemy);
        }       
    }

    private void PrepareEnemy(Enemy enemy)
    {
        enemy.Reset();
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
        var minBounds = gameField.bounds.min;
        var maxBounds = gameField.bounds.max;

        var randomX = Random.Range(minBounds.x, maxBounds.x);        
        var randomZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(randomX, 0, randomZ);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}