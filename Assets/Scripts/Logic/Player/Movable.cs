using System;
using UnityEngine;

public class Movable : IMove
{         
    public void Move(Transform transform, Vector3 direction, float speed, float delta)
    {
       transform.position += direction * speed * delta; 
    }
}
