using System.Collections.Generic;
using Data.Interfaces.Models;

namespace Data.Interfaces
{
    public interface IGameEntities
    {
        IReadOnlyCollection<IEnemyModel> EnemyModels { get; }
    }
}