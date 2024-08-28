using R3;

namespace Logic.Interfaces.Providers
{
    public interface IEnemySpawnSettingsProvider
    {
        ReactiveProperty<bool> IsSettingLoadedRx { get; }

        float GetChanceForSpawn(int enemyCount);
    }
}