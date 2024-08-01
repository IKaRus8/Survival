using R3;
using UnityEngine;

public interface  IPlayerHolder 
{
    ReactiveProperty<IPlayer> PlayerRx { get; }
}
