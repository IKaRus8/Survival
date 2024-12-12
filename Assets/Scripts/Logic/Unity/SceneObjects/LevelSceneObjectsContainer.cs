using Logic.Interfaces.Services.Level;
using UnityEngine;

namespace Logic.Unity.SceneObjects
{
    public class LevelSceneObjectsContainer : BaseSceneObjectsContainer, ILevelSceneObjectContainer
    {
        [SerializeField] 
        private Transform _gridParent;
    
        public Transform GridParent => _gridParent;

        protected override void Awake()
        {
            base.Awake();

            IBaseSceneObjectContainer.PopupContainer = _popupContainer;
        }
    }
}