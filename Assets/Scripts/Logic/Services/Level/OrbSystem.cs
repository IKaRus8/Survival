using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity.Enemy;
using R3;
using UnityEngine;
using Utilities.Extensions;

namespace Logic.Services.Level
{
    public class OrbSystem : IDisposable
    {
        private const int MaxAttempts = 10;
        
        private readonly OrbSpawnConfig _config;
        private readonly IDisposable _disposable;
        private readonly Queue<string> _spawnAttempts;
        private readonly System.Random _random;
        private readonly IOrbSpawner _orbSpawner;

        public OrbSystem(
            IOrbSpawnConfigProvider configProvider,
            IEnemyDeathObserver enemyDeathObserver,
            IOrbSpawner orbSpawner)
        {
            _orbSpawner = orbSpawner;
            _config = configProvider.Config;
            _spawnAttempts = new Queue<string>();
            _random = new System.Random();

            _disposable = enemyDeathObserver.EnemyDeadRx.Subscribe(OnEnemyDead);
        }

        private void OnEnemyDead(IEnemy enemy)
        {
            var orbId = ChooseOrb();
            
            RegisterSpawn(orbId);
            
            if (!orbId.IsNullOrEmpty())
            {
                Spawn(orbId, enemy.Position);
            }
        }

        private string ChooseOrb()
        {
            // Подсчитываем текущее количество успешных спавнов
            var spawnCounts = _config.OrbSpawnParameters
                .ToDictionary(p => p.Id, p => _spawnAttempts.Count(id => id == p.Id));

            foreach (var param in _config.OrbSpawnParameters.OrderByDescending(p => p.Chance))
            {
                int allowedCount = Mathf.RoundToInt(param.Chance * MaxAttempts);
                int currentCount = spawnCounts[param.Id];
                int remainingAllowed = allowedCount - currentCount;

                int remainingAttempts = MaxAttempts - _spawnAttempts.Count;
                if (remainingAttempts <= 0) remainingAttempts = MaxAttempts;

                float adjustedChance = (float)remainingAllowed / remainingAttempts;
                if (_random.NextDouble() < adjustedChance)
                {
                    return param.Id;
                }
            }

            return string.Empty;

        }

        private void RegisterSpawn(string id)
        {
            if (_spawnAttempts.Count >= MaxAttempts)
            {
                _spawnAttempts.Clear();
            }

            _spawnAttempts.Enqueue(id);
        }

        private void Spawn(string id, Vector3 position)
        {
            Debug.Log($"[OrbSystem] Spawned orb: {id}");
            
            _orbSpawner.SpawnOrb(id, position);
        }

        // Опционально: метод для теста выпадений
        public void Simulate(int count = 1000)
        {
            for (var i = 0; i < count; i++)
            {
                OnEnemyDead(null);
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}