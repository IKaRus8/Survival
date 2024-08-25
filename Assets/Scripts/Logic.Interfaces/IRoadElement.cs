using System;
using UnityEngine;

public interface IRoadElement
{  
    void SetPosition(Vector3 position);  
    Transform Transform { get; set; }
    public Action<IRoadElement> OnPlayerEnter { get; set; }
    public bool IsPlayerInside { get; }
    void Reset();
}
