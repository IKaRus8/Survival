using UnityEngine;

public class SceneObjectContainer : MonoBehaviour, ISceneObjectContainer
{
    [SerializeField] 
    private Transform _roadParent;

    [SerializeField]
    private RectTransform _popupContainer;
    
    public Transform RoadParent => _roadParent;
    public RectTransform PopupContainer => _popupContainer;
}
