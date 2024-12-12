using UnityEngine;

namespace Logic.Interfaces.Unity.Player
{
    public interface IDefendObject : IDamageable
    {
        Vector3 Position { get; }
    }
}