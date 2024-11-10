using R3;

namespace Logic.Interfaces.Services.Player
{
    public interface IPlayerTargetObserver
    {
        ReactiveProperty<IEnemy> TargetRx { get; }
    }
}