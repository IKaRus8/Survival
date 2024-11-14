using R3;

namespace Logic.Interfaces.Providers.Enemies
{
    public interface IEnemySpawnSettingsProvider
    {
        ReactiveProperty<bool> IsSettingLoadedRx { get; }

        float GetChanceForSpawn();
    }
}