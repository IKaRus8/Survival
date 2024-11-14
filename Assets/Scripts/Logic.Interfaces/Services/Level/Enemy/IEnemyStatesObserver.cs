using System.Collections.Generic;
using Logic.RuntimeData;
using R3;

namespace Logic.Interfaces.Services.Level.Enemy
{
    public interface IEnemyStatesObserver
    {
        Subject<IReadOnlyCollection<EnemyStateModel>> EnemyStatesUpdated { get; }
    }
}