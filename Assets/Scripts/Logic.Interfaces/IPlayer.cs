using R3;
using UnityEngine;
using UnityEngine.Rendering;

public interface IPlayer: IDamageble 
{   
    void Move(Vector3 direction, float speed, float time);
    void Rotate(Vector3 direction, float speed, float time);    

    void Die();

    bool IsDead { get; }

    float Speed { get; }
    float RotateSpeed { get; }
    Transform Transform { get; }

    ReactiveProperty<bool> IsRotating { get; }
    Transform WeaponShootPoint { get; }
}
