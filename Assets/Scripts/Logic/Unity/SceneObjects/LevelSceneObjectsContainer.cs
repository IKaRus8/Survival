using Logic.Interfaces.Services.Level;
using UnityEngine;

namespace Logic.Unity.SceneObjects
{
    public class LevelSceneObjectsContainer : BaseSceneObjectsContainer, ILevelSceneObjectContainer
    {
        [SerializeField] 
        private Transform _gridParent;
        [SerializeField]
        private RectTransform _popupContainer;
        [SerializeField]
        private Transform _enemiesContainer;
    
        public Transform GridParent => _gridParent;
        public Transform EnemiesContainer => _enemiesContainer;

        private void Awake()
        {
            PopupContainer = _popupContainer;
        }
    }
}