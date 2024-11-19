using R3;

namespace Logic.Interfaces.Providers.Level.Enemies
{
    public interface IEnemySpawnSettingsProvider
    {
        ReactiveProperty<bool> IsSettingLoadedRx { get; }

        float GetChanceForSpawn();
    }
}