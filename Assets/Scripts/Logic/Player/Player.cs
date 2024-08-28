using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{  
    public float Speed => movingSettings.BaseSpeed;

    public MovingSettings movingSettings;
    
    private Transform _transform;

    public Transform Transform => _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public  void Move(Vector3 direction, float speed, float delta)
    {
        _transform.position += direction * speed * delta;      
    }
}
