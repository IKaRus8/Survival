using System;
using UnityEngine;

public interface IGridElement
{  
    void SetPosition(Vector3 position);
    BoxCollider Collider { get; set; }
    Transform Transform { get; set; }
    public Action<IGridElement> OnPlayerEnter { get; set; }
    public bool IsPlayerInside { get; }
    void Reset();
}
