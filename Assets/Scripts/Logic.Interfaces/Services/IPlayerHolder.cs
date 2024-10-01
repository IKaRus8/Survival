using R3;

namespace Logic.Interfaces
{
    public interface  IPlayerHolder 
    {
        ReactiveProperty<IPlayer> PlayerRx { get; }
    }
}
