using System.Linq;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Logic.Interfaces.Providers;
using R3;

namespace Logic.Providers
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider
    {
        private const string EnemySpawnSettingsKey = "EnemySpawnSettings";
        
        private readonly IAssetService _assetService;
        
        private EnemySpawnSettings _settings;
        
        public ReactiveProperty<bool> IsSettingLoadedRx { get; } 

        public EnemySpawnSettingsProvider(IAssetService assetService)
        {
            _assetService = assetService;
            IsSettingLoadedRx = new ReactiveProperty<bool>();
            
            LoadSettings().Forget();
        }

        public float GetChanceForSpawn(int enemyCount)
        {
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
    }
}