using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface ILevelSceneObjectContainer
    {
        Transform GridParent { get; }
        Transform EnemiesContainer { get; }
        Transform LevelContainer { get; }
    }
}