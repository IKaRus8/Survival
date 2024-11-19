using Data.Interfaces.Models;

namespace Logic.Interfaces.Providers.Level.Enemies
{
    public interface IEnemyModelsProvider
    {
        IEnemyModel GetEnemyModel(string id);
    }
}