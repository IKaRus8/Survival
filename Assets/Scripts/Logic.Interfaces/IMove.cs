using UnityEngine;

public interface IMove 
{
    public virtual void Move(Transform transform, Vector3 direction, float speed, float delta){}
    
}
