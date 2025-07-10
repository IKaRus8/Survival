using Logic.Interfaces.Unity.Enemy;
using R3;

namespace Logic.Interfaces.Services.Level.Enemy
{
    public interface IEnemyDeathObserver
    {
        Subject<IEnemy> EnemyDeadRx { get; }
    }
}