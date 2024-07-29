using R3;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IPlayer
{  
    public float Speed => movingSettings.BaseSpeed;

    public MovingSettings movingSettings;    
   
    public Transform Transform => transform;

    public  void Move(Vector3 direction, float speed, float delta)
    {
        transform.position += direction * speed * delta;      
    }
}
