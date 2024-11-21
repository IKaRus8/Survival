using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Enemy
{
    public class EnemySpawner : IDisposable
    {
        private readonly IGridSystem _gridSystem;
        private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
        private readonly IEnemyFactory _factory;
        private readonly IEnemyModelsProvider _enemyModelsProvider;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _settingDisposable;

        private GameObject _enemyPrefab;
        private IDisposable _spawnDisposable;

        public EnemySpawner(
            IGridSystem gridSystem,
            IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
            IEnemyFactory factory,
            IEnemyModelsProvider enemyModelsProvider,
            IEnemyProvider enemyProvider)
        {
            _gridSystem = gridSystem;
            _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
            _factory = factory;
            _enemyModelsProvider = enemyModelsProvider;
            _enemyProvider = enemyProvider;
            
            StartSpawn(true);
        }

        private void StartSpawn(bool value)
        {
            if (value)
            {
                if (_spawnDisposable == null)
                {
                    _spawnDisposable = Observable.Interval(TimeSpan.FromSeconds(1f))
                        .Subscribe(SpawnProcess);
                }
            }
            else
            {
                _spawnDisposable?.Dispose();
                _spawnDisposable = null;
            }
        }

        private void SpawnProcess(Unit _)
        {
            SpawnEnemy(Constants.Enemy.Id.SimpleEnemy).Forget();
        }

        private async UniTaskVoid SpawnEnemy(string id)
        {
            var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn();

            var random = RandomHelper.GetChance();

            if (random > spawnProbability)
            {
                return;
            }
            
            var enemy = _enemyProvider.DeadEnemies.FirstOrDefault(e => e.Id == id);

            if (enemy == null)
            {
                enemy = await _factory.CreateAsync(id);

                var model = _enemyModelsProvider.GetEnemyModel(id);
                
                enemy.Initialize(model);
            }

            PrepareEnemy(enemy);
        }

        private void PrepareEnemy(IEnemy enemy)
        {
            enemy.Reset();

            enemy.MoveTo(GetEnemyPosition());

            AddEnemy(enemy);
        }

        private void AddEnemy(IEnemy enemy)
        {
            _enemyProvider.AddEnemy(enemy);
        }

        private Vector3 GetEnemyPosition()
        {
            var gridElementRectangle = _gridSystem.GetRandomGridPlaneWithOutHero().ElementRectangle;

            return gridElementRectangle.RandomPosition;
        }

        public void Dispose()
        {
            _spawnDisposable?.Dispose();
            _settingDisposable?.Dispose();
        }
    }
}