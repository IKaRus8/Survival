using UnityEngine;

public interface ISceneObjectContainer 
{
    Transform RoadParent { get; }
    Object EnemyPrefab { get; }
}
