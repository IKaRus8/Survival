using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface IBaseSceneObjectContainer
    {
        Transform EnemiesContainer { get; }
        Transform LevelContainer { get; }
        Transform VfxContainer { get; }
        static RectTransform PopupContainer { get; protected set; }
    }
}