using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.Interfaces.Models;
using Logic.Interfaces.Providers.Level.Hero;

namespace Logic.Providers.Level.Hero
{
    public class HeroModelsProvider : IHeroModelsProvider
    {
        private readonly IReadOnlyCollection<IHeroModel> _models;

        public HeroModelsProvider(IGameEntities gameEntities)
        {
            _models = gameEntities.HeroModels;
        }

        public IHeroModel GetHeroModel(string id)
        {
            var result = _models.FirstOrDefault(m => m.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Hero {id} not found");
            }

            return result;
        }
    }
}