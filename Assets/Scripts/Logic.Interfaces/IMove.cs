using UnityEngine;

public interface IMove 
{
    void Move(Transform transform, Vector3 direction, float speed, float delta);    
}
