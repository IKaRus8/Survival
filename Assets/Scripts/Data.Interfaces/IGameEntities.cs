using System.Collections.Generic;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;

namespace Data.Interfaces
{
    public interface IGameEntities
    {
        IReadOnlyCollection<IEnemyModel> EnemyModels { get; }
        IReadOnlyCollection<IHeroModel> HeroModels { get; }
        IReadOnlyCollection<IAttackModel> AttackModels { get; }
    }
}