using R3;
using System;
using UnityEngine;

public class RoadElement : MonoBehaviour, IRoadElement
{   
    private Transform _playerTransform;
    Bounds bounds;
   
    public bool IsPlayerInside { get; set; }
    public bool IsOnRightPosition { get; set; }

    [SerializeField] private Transform _transform;
    public Transform Transform
    {
        get => _transform;
        set => _transform = value;
    }
    public Action<IRoadElement> OnPlayerEnter { get; set; }

    [SerializeField] private BoxCollider _collider;
    

    public void Setup(Transform playerTransform)
    {
        _playerTransform = playerTransform;              
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           PlayerEnter();
        }
    }


    public void PlayerEnter()
    {
        IsPlayerInside = true;      
        OnPlayerEnter?.Invoke(this);
    }

    public void RefreshCollider()
    {
        IsPlayerInside = false;       
    }
}

public interface IRoadElement
{
    bool IsPlayerInside { get; set; }
    bool IsOnRightPosition { get; set; }    
    void SetPosition(Vector3 position);
    void RefreshCollider();
    void PlayerEnter();
    Transform Transform { get; set; }
    public Action<IRoadElement> OnPlayerEnter { get; set; }
    public void Setup(Transform playerTransform);
}