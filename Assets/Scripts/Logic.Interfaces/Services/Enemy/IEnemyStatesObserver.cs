using System.Collections.Generic;
using Logic.RuntimeData;
using R3;

namespace Logic.Interfaces.Services.Enemy
{
    public interface IEnemyStatesObserver
    {
        Subject<IReadOnlyCollection<EnemyStateModel>> EnemyStatesUpdated { get; }
    }
}