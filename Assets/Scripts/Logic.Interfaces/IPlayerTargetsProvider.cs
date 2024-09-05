using UnityEngine;
using R3;

namespace Logic.Providers
{
    public interface IPlayerTargetsProvider
    {
        ReactiveProperty<Enemy> TargetRX { get; }
        void InitTargetsObserv(IPlayer player);
    }
}