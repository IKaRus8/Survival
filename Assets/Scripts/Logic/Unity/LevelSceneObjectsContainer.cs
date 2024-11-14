using Logic.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Unity
{
    public class LevelSceneObjectsContainer : MonoBehaviour, ILevelSceneObjectContainer
    {
        [SerializeField] 
        private Transform _gridParent;
        [SerializeField]
        private RectTransform _popupContainer;
        [SerializeField]
        private Transform _enemiesContainer;
    
        public Transform GridParent => _gridParent;
        public RectTransform PopupContainer => _popupContainer;
        public Transform EnemiesContainer => _enemiesContainer;
    }
}