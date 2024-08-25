using UnityEngine;

public class SceneObjectContainer : MonoBehaviour, ISceneObjectContainer
{
    [SerializeField] private Transform _roadParent;  
    Transform ISceneObjectContainer.RoadParent => _roadParent;
}
