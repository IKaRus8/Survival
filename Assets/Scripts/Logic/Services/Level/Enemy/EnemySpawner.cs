using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Providers.Enemies;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using R3;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic.Services.Level.Enemy
{
    public class EnemySpawner : IDisposable
    {
        private readonly IGridController _gridController;
        private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
        private readonly CompositeDisposable _disposables;
        private readonly IAssetService _assetService;
        private readonly IInstantiator _instantiator;
        private readonly IEnemyModelsProvider _enemyModelsProvider;
        private readonly IEnemyProvider _enemyProvider;
        private readonly Transform _enemiesContainer;

        private GameObject _enemyPrefab;

        public EnemySpawner(
            IGridController gridController,
            IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
            IAssetService assetService,
            ILevelSceneObjectContainer levelSceneObjectContainer,
            IEnemyModelsProvider enemyModelsProvider,
            IEnemyProvider enemyProvider)
        {
            _gridController = gridController;
            _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
            _assetService = assetService;
            _enemyModelsProvider = enemyModelsProvider;
            _enemyProvider = enemyProvider;

            _disposables = new CompositeDisposable();
            _enemiesContainer = levelSceneObjectContainer.EnemiesContainer;
            
            GetPrefabAsync().Forget();
        }

        private async UniTaskVoid GetPrefabAsync()
        {
            

            _enemySpawnSettingsProvider.IsSettingLoadedRx.Subscribe(StartSpawn).AddTo(_disposables);
        }

        private void StartSpawn(bool value)
        {
            if (!value)
            {
                return;
            }

            Observable.Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(SpawnProcess)
                .AddTo(_disposables);
        }

        private void SpawnProcess(Unit _)
        {
            SpawnEnemy(Constants.EnemyConstants.Ids.SimpleEnemy).Forget();
        }

        private async UniTaskVoid SpawnEnemy(string id)
        {
            var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn();

            var random = RandomHelper.GetRandomFloat();

            if (random > spawnProbability)
            {
                return;
            }
            
            var enemy = _enemyProvider.DeadEnemies.FirstOrDefault(e => e.Model.Id == id);

            if (enemy == null)
            {
                enemy = await _assetService
                    .LoadAndInstantiateAsync<IEnemy>(id, _enemiesContainer);
            }

            PrepareEnemy(enemy);
        }

        private void PrepareEnemy(IEnemy enemy)
        {
            enemy.Reset();

            enemy.MoveTo(GetEnemyPos());

            AddEnemy(enemy);
        }

        private void AddEnemy(IEnemy enemy)
        {
            _enemyProvider.AddEnemy(enemy);
        }

        private Vector3 GetEnemyPos()
        {
            var gridElementCollider = _gridController.GetRandomGridPlaneWithOutPlayer().Collider;

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
}