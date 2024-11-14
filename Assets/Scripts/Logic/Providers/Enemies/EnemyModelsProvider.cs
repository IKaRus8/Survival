using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.Interfaces.Models;
using Logic.Interfaces.Providers.Enemies;

namespace Logic.Providers.Enemies
{
    public class EnemyModelsProvider : IEnemyModelsProvider
    {
        private readonly IReadOnlyCollection<IEnemyModel> _models;

        public EnemyModelsProvider(IGameEntities gameEntities)
        {
            _models = gameEntities.EnemyModels;
        }

        public IEnemyModel GetEnemyModel(string id)
        {
            var result = _models.FirstOrDefault(m => m.Id == id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Enemy {id} not found");
            }

            return result;
        }
    }
}