using R3;
using System;
using UnityEngine;

public class RoadElement : MonoBehaviour, IRoadElement
{          
    
    [SerializeField] private Transform _transform;

    public Transform Transform
    {
        get => _transform;
        set => _transform = value;
    }
    public Action<IRoadElement> OnPlayerEnter { get; set; }

    [SerializeField] private BoxCollider _collider; 

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
        OnPlayerEnter?.Invoke(this);
    }  
}

   