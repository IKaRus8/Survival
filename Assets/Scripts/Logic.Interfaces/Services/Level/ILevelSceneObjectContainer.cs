using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface ILevelSceneObjectContainer
    {
        Transform GridParent { get; }
        RectTransform PopupContainer { get; }
        Transform EnemiesContainer { get; }
    }
}