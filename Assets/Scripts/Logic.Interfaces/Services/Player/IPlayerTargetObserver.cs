using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using R3;

namespace Logic.Interfaces.Services.Player
{
    public interface IPlayerTargetObserver
    {
        ReactiveProperty<IEnemy> TargetRx { get; }
    }
}