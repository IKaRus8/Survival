using System.Linq;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers;
using Logic.Interfaces.Providers.Enemies;
using Logic.Interfaces.Services;
using R3;
using Settings;

namespace Logic.Providers.Enemies
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider
    {
        private const string EnemySpawnSettingsKey = "EnemySpawnSettings";

        private readonly IAssetService _assetService;
        private readonly IEnemyProvider _enemyProvider;

        private EnemySpawnSettings _settings;

        public ReactiveProperty<bool> IsSettingLoadedRx { get; }

        public EnemySpawnSettingsProvider(
            IAssetService assetService,
            IEnemyProvider enemyProvider)
        {
            _assetService = assetService;
            _enemyProvider = enemyProvider;
            IsSettingLoadedRx = new ReactiveProperty<bool>();

            LoadSettings().Forget();
        }

        public float GetChanceForSpawn()
        {
            var enemyCount = _enemyProvider.AliveEnemyCount;

            foreach (var enemyParameter in _settings.SpawnParameters.OrderBy(p => p.Quantity))
            {
                if (enemyCount < enemyParameter.Quantity)
                {
                    return enemyParameter.Chance;
                }
            }

            return 0f;
        }

        private async UniTaskVoid LoadSettings()
        {
            _settings = await _assetService.LoadAssetAsync<EnemySpawnSettings>(EnemySpawnSettingsKey);

            IsSettingLoadedRx.Value = true;
        }
    }
}