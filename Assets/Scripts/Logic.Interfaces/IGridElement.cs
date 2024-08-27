using System;
using UnityEngine;

public interface IGridElement
{  
    void SetPosition(Vector3 position);  
    Transform Transform { get; set; }
    public Action<IGridElement> OnPlayerEnter { get; set; }
    public bool IsPlayerInside { get; }
    void Reset();
}
