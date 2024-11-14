using UnityEngine;

namespace Logic.Interfaces
{
    public interface ILevelSceneObjectContainer
    {
        Transform GridParent { get; }
        RectTransform PopupContainer { get; }
        Transform EnemiesContainer { get; }
    }
}