using System.Linq;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Logic.Interfaces.Providers;
using R3;
using System;
using Logic.Interfaces;

namespace Logic.Providers
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider, IDisposable
    {
        private const string EnemySpawnSettingsKey = "EnemySpawnSettings";

        private readonly IAssetService _assetService;
        private readonly IAliveEnemyProvider _enemyProvider;

        private EnemySpawnSettings _settings;

        public ReactiveProperty<bool> IsSettingLoadedRx { get; }

        public EnemySpawnSettingsProvider(
            IAssetService assetService,
            IAliveEnemyProvider enemyProvider)
        {
            _assetService = assetService;
            _enemyProvider = enemyProvider;
            IsSettingLoadedRx = new ReactiveProperty<bool>();

            LoadSettings().Forget();
        }

        public float GetChanceForSpawn()
        {
            var enemyCount = _enemyProvider.AliveEnemyCount;

            foreach (var enemyParameter in _settings.SpawnParameters.OrderBy(p => p.enemyQuantity))
            {
                if (enemyCount < enemyParameter.enemyQuantity)
                {
                    return enemyParameter.spawnChance;
                }
            }

            return 0f;
        }

        private async UniTaskVoid LoadSettings()
        {
            _settings = await _assetService.GetAssetAsync<EnemySpawnSettings>(EnemySpawnSettingsKey);

            IsSettingLoadedRx.Value = true;
        }

        public void Dispose()
        {
            IsSettingLoadedRx.Value = false;
            IsSettingLoadedRx.Dispose();
        }
    }
}