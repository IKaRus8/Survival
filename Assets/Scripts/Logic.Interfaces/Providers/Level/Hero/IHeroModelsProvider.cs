using Data.Interfaces.Models;

namespace Logic.Interfaces.Providers.Level.Hero
{
    public interface IHeroModelsProvider
    {
        IHeroModel GetHeroModel(string heroId);
    }
}