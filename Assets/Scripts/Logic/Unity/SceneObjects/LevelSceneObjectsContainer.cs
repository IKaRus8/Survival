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
        [SerializeField]
        private Transform _vfxContainer;
    
        public Transform GridParent => _gridParent;
        public Transform EnemiesContainer => _enemiesContainer;
        public Transform VfxContainer => _vfxContainer;
        public Transform LevelContainer { get; private set; }

        private void Awake()
        {
            LevelContainer = transform;
            
            PopupContainer = _popupContainer;
        }
    }
}