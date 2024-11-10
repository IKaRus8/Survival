using R3;

namespace Logic.Interfaces.Services.Player
{
    public interface  IPlayerHolder 
    {
        ReactiveProperty<IPlayer> PlayerRx { get; }
    }
}
