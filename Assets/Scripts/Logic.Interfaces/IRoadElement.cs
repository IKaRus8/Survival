using System;
using UnityEngine;

public interface IRoadElement
{
  
    void SetPosition(Vector3 position);   
    void PlayerEnter();
    Transform Transform { get; set; }
    public Action<IRoadElement> OnPlayerEnter { get; set; }
}
