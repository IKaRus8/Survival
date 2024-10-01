using R3;
using UnityEngine;

namespace Logic.Interfaces
{
    public interface IPlayer: IDamageble 
    {   
        void Move(Vector3 direction, float speed, float time);
        void Rotate(Vector3 direction, float speed, float time);    
        bool IsDead { get; }
        float Speed { get; }
        float RotateSpeed { get; }
        Transform Transform { get; }
        ReactiveProperty<bool> IsRotating { get; }
        Transform WeaponShootPoint { get; }
    
        void Die();
    }
}
