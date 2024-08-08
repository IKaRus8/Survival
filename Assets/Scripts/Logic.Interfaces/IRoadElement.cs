using System;
using UnityEngine;

public interface IRoadElement
{
    bool IsPlayerInside { get; set; }
    bool IsOnRightPosition { get; set; }
    void SetPosition(Vector3 position);
    void RefreshCollider();
    void PlayerEnter();
    Transform Transform { get; set; }
    public Action<IRoadElement> OnPlayerEnter { get; set; }
}
