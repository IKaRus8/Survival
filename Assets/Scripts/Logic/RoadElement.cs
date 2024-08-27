using System;
using UnityEngine;

public class RoadElement : MonoBehaviour, IGridElement
{         
    [SerializeField] private Transform _transform;
    [SerializeField] private BoxCollider _collider;

    public Transform Transform
    {
        get => _transform;
        set => _transform = value;
    }
    public Action<IGridElement> OnPlayerEnter { get; set; }
    public bool IsPlayerInside { get=> isPlayerInside;  }

    private bool isPlayerInside = false;
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

    public void Reset()
    {
        isPlayerInside = false;
    }

    private void PlayerEnter()
    {       
        OnPlayerEnter?.Invoke(this);
        isPlayerInside = true;
    }  
}

   