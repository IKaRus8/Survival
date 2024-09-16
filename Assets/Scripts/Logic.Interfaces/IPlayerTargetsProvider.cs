using UnityEngine;
using R3;

namespace Logic.Providers
{
    public interface IPlayerTargetsProvider
    {
        ReactiveProperty<IEnemy> TargetRX { get; }
        void InitTargetsObserv(IPlayer player);
    }
}