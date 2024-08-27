using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Logic.Interfaces.Providers;

namespace Logic.Providers
{
    public class EnemySpawnSettingsProvider : IEnemySpawnSettingsProvider
    {
        private const string EnemySpawnSettingsKey = "EnemySpawnSettings";
        
        private readonly IAssetService _assetService;
        
        private EnemySpawnSettings _settings;

        public EnemySpawnSettingsProvider(IAssetService assetService)
        {
            _assetService = assetService;
            
            LoadSettings().Forget();
        }

        public List<EnemySpawnSettings.SpawnParameter> GetAllParameters()
        {
            return _settings.SpawnParameters;
        }

        private async UniTaskVoid LoadSettings()
        {
            _settings = await _assetService.GetAssetAsync<EnemySpawnSettings>(EnemySpawnSettingsKey);
        }
    }
}