using UnityEngine;

namespace Logic.Interfaces
{
    public interface ISceneObjectContainer
    {
        Transform RoadParent { get; }
        RectTransform PopupContainer { get; }
    }
}