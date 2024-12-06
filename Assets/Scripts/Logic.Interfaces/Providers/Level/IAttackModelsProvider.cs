using Data.Interfaces.Models.Attack;

namespace Logic.Interfaces.Providers.Level
{
    public interface IAttackModelsProvider
    {
        IAttackModel GetAttackModel(string id);
    }
}