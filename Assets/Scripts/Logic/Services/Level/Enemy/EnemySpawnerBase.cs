using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity.Enemy;
using R3;
using UnityEngine;

namespace Logic.Services
{
    public abstract class EnemySpawnerBase : IPauseHandler, IDisposable
    {
        protected abstract string EnemyId { get; }
        protected abstract string SpawnSettingsId { get; }
        
        private readonly IEnemySpawnSettingsProvider _enemySpawnSettingsProvider;
        private readonly IEnemyFactory _factory;
        private readonly IEnemyModelsProvider _enemyModelsProvider;
        private readonly IAttackModelsProvider _attackModelsProvider;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IDisposable _settingDisposable;

        private GameObject _enemyPrefab;
        private IDisposable _spawnDisposable;

        protected EnemySpawnerBase(
            IEnemySpawnSettingsProvider enemySpawnSettingsProvider,
            IEnemyFactory factory,
            IEnemyModelsProvider enemyModelsProvider,
            IAttackModelsProvider attackModelsProvider,
            IEnemyProvider enemyProvider)
        {
            _enemySpawnSettingsProvider = enemySpawnSettingsProvider;
            _factory = factory;
            _enemyModelsProvider = enemyModelsProvider;
            _attackModelsProvider = attackModelsProvider;
            _enemyProvider = enemyProvider;
            
            StartSpawn();
        }

        public void Pause()
        {
            _spawnDisposable?.Dispose();
        }

        public void Resume()
        {
            StartSpawn();
        }

        protected abstract Vector3 GetSpawnPosition();

        private void StartSpawn()
        {
            _spawnDisposable?.Dispose();
            
            _spawnDisposable = Observable.Interval(_enemySpawnSettingsProvider.SpawnCooldown)
                .Subscribe(SpawnProcess);
        }

        private void SpawnProcess(Unit _)
        {
            SpawnEnemy().Forget();
        }

        private async UniTaskVoid SpawnEnemy()
        {
            var spawnProbability = _enemySpawnSettingsProvider.GetChanceForSpawn();

            var random = RandomHelper.GetChance();

            if (random > spawnProbability)
            {
                return;
            }
            
            var enemy = _enemyProvider.DeadEnemies.FirstOrDefault(e => e.Id == EnemyId);

            if (enemy == null)
            {
                enemy = await _factory.CreateAsync(EnemyId);

                var model = _enemyModelsProvider.GetEnemyModel(EnemyId);
                var attackModel = _attackModelsProvider.GetAttackModel(model.AttackModelId);
                
                enemy.Initialize(model, attackModel);

                AddEnemy(enemy);
            }

            PrepareEnemy(enemy);
        }

        private void PrepareEnemy(IEnemy enemy)
        {
            enemy.MoveTo(GetSpawnPosition());
            
            enemy.ReInitialize();
        }

        private void AddEnemy(IEnemy enemy)
        {
            _enemyProvider.AddEnemy(enemy);
        }

        public void Dispose()
        {
            _spawnDisposable?.Dispose();
            _settingDisposable?.Dispose();
        }
    }
}