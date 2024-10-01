using R3;

namespace Logic.Interfaces
{
    public interface IPlayerTargetObserver
    {
        ReactiveProperty<IEnemy> TargetRx { get; }
    }
}