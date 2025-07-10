using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface ILevelSceneObjectContainer : IBaseSceneObjectContainer
    {
        Transform GridParent { get; }
        Transform OrbsContainer { get; }
    }
}