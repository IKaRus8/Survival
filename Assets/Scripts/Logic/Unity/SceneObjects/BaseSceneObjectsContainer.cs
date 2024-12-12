using UnityEngine;

namespace Logic.Unity.SceneObjects
{
    public class BaseSceneObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        protected RectTransform _popupContainer;
        [SerializeField]
        protected Transform _enemiesContainer;
        [SerializeField]
        protected Transform _vfxContainer;
        
        public Transform EnemiesContainer => _enemiesContainer;
        public Transform VfxContainer => _vfxContainer;
        public Transform LevelContainer { get; private set; }

        protected virtual void Awake()
        {
            LevelContainer = transform;
        }
    }
}