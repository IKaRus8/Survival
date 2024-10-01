using System;
using UnityEngine;

namespace Logic.Interfaces
{
    public interface IGridElement
    {
        BoxCollider Collider { get; }
        Transform Transform { get; }
        bool IsPlayerInside { get; }
        Action<IGridElement> OnPlayerEnter { get; set; }
        
        void Reset();
        void SetPosition(Vector3 position);
    }
}