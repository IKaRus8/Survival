using R3;
using System;
using UnityEngine;

public class RoadElement : MonoBehaviour, IRoadElement
{
    private Vector2Int _index;
    private Transform _playerTransform;
    Bounds bounds;
    public Vector2Int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }
    public bool IsPlayerInside { get; set; }

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
        bounds = _collider.bounds;
        Observable.EveryUpdate().Subscribe(CheckPalyerPosition).AddTo(_collider);
    }

    public void SetIndex(Vector2Int index)
    {
        _index = index;      
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void CheckPalyerPosition(Unit _)
    {
        if ((_playerTransform.position.x <= bounds.max.x && _playerTransform.position.x >= bounds.min.x) &&
            (_playerTransform.position.z <= bounds.max.z && _playerTransform.position.z >= bounds.min.z))
        {
            if (!IsPlayerInside)
            {
                PlayerEnter();
            }            
        }
        else
        {
            RefreshCollider();
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
    public Vector2Int Index { get; set; }
    void SetIndex(Vector2Int index);
    void SetPosition(Vector3 position);
    void RefreshCollider();
    void PlayerEnter();
    Transform Transform { get; set; }
    public Action<IRoadElement> OnPlayerEnter { get; set; }
    public void Setup(Transform playerTransform);
}