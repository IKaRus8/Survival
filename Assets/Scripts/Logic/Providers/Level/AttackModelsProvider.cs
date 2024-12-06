using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.Interfaces.Models.Attack;
using Logic.Interfaces.Providers.Level;

namespace Logic.Providers.Level
{
    public class AttackModelsProvider : IAttackModelsProvider
    {
        private readonly Dictionary<string, IAttackModel> _models;
        
        public AttackModelsProvider(IGameEntities gameEntities)
        {
            _models = gameEntities.AttackModels.ToDictionary(m => m.ID);
        }

        public IAttackModel GetAttackModel(string id)
        {
            if (_models.TryGetValue(id, out var model))
            {
                return model;
            }
            
            throw new NullReferenceException($"Model with ID {id} not found");
        }
    }
}