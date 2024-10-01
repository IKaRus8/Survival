using System.Collections.Generic;
using Data.Interfaces;
using Data.Interfaces.Constants;
using Data.Interfaces.Models;
using Data.Models;

namespace Data
{
    public class GameEntities : IGameEntities
    {
        public IReadOnlyCollection<IEnemyModel> EnemyModels { get; }

        public GameEntities()
        {
            EnemyModels = GetEnemyModels();
        }

        private IEnemyModel[] GetEnemyModels()
        {
            return new IEnemyModel[]
            {
                new EnemyModel(
                    Constants.EnemyConstants.Ids.SimpleEnemy,
                    3f,
                    1f,
                    5f,
                    10f,
                    100f),
                new EnemyModel(
                    Constants.EnemyConstants.Ids.SimpleEnemy + "2",
                    3f,
                    10f,
                    5f,
                    100f,
                    100f)
            };
        }
    }
}