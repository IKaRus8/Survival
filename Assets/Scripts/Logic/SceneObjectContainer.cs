using UnityEngine;

public class SceneObjectContainer : MonoBehaviour, ISceneObjectContainer
{   
    [SerializeField] private Transform _roadParent;
    [SerializeField] private Object _enemyPrefab;       

    Transform ISceneObjectContainer.RoadParent => _roadParent;  
    Object ISceneObjectContainer.EnemyPrefab => _enemyPrefab;   
}
