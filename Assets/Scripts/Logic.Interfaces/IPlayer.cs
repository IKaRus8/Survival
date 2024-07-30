using UnityEngine;
using UnityEngine.Rendering;

public interface IPlayer 
{   
    void Move(Vector3 direction, float speed, float time);    
    float Speed { get; }
    Transform Transform { get; }
}
