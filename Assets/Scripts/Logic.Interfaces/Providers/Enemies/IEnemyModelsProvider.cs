using Data.Interfaces.Models;

namespace Logic.Interfaces.Providers.Enemies
{
    public interface IEnemyModelsProvider
    {
        IEnemyModel GetEnemyModel(string id);
    }
}