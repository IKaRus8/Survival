using System;
using UnityEngine;

public class GridElement : MonoBehaviour, IGridElement
{         
    [SerializeField] private Transform _transform;
    [SerializeField] private BoxCollider _collider;

    public Transform Transform
    {
        get => _transform;
        set => _transform = value;
    }
    public BoxCollider Collider
    {
        get => _collider;
        set => _collider = value;
    }

    public Action<IGridElement> OnPlayerEnter { get; set; }
    public bool IsPlayerInside => isPlayerInside;  
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

   