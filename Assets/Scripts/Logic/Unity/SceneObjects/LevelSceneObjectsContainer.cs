using Logic.Interfaces.Services.Level;
using UnityEngine;

namespace Logic.Unity.SceneObjects
{
    public class LevelSceneObjectsContainer : BaseSceneObjectsContainer, ILevelSceneObjectContainer
    {
        [SerializeField] 
        private Transform _gridParent;
        [SerializeField]
        private Transform _orbsContainer;
    
        public Transform GridParent => _gridParent;
        public Transform OrbsContainer => _orbsContainer;

        protected override void Awake()
        {
            base.Awake();

            IBaseSceneObjectContainer.PopupContainer = _popupContainer;
        }
    }
}