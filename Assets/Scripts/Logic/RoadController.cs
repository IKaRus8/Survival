using R3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class RoadController: IDisposable
{
    private List<IRoadElement> _roads = new(); 
    private List<IRoadElement> _roadsInRightPos = new();   
    private List<IRoadElement> _roadsInWrongPos = new();
    
    private IInstantiator _container;
    private IAssetService _assetService;   
    private ReactiveProperty <IRoadElement> _currentElementRX;
    private float offset=50;    
    private Transform _playerTransform;
    private CompositeDisposable _disposables;
    private int countInMap = 9;
    private Vector3[] positions;

    public RoadController(IInstantiator installer, IAssetService assetService, IPlayerHolder playerHolder)
    {
        _container = installer;
        _assetService = assetService;       
        _disposables = new();
        Init();
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
    }

    private void OnPlayerCreated(IPlayer player)
    {
        if (player == null)
        {
            return;
        }
        _playerTransform = player.Transform;   
        SetupRoads();        
    }

    private void SetupRoads()
    {
        foreach (var road in _roads)
        {
            road.Setup(_playerTransform);
        }
    }


    public void Init()
    {
        CreatePositions();
        CreateStartField();
    }

    private void CreatePositions()
    {
      Vector3[] positions = new Vector3[]
        {
            new Vector3(-offset, 0, 0),
            new Vector3(offset, 0, 0),
            new Vector3(0, 0, offset),
            new Vector3(0, 0, -offset),
            new Vector3(offset, 0, offset),
            new Vector3(-offset, 0, -offset),
            new Vector3(offset, 0, -offset),
            new Vector3(-offset, 0, offset)
        };
    }

    private async void CreateStartField()
    {

        var roadParent = _container.CreateEmptyGameObject("Road");       
        roadParent.transform.position = Vector3.zero;
        var roadPrefab = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Plane.prefab");
        BuildStartRoad(roadPrefab, roadPrefab.transform);
    }

    private  void BuildStartRoad(GameObject roadPrefab, Transform roadParent)
    {
        var currentRoad = _container.InstantiatePrefabForComponent<IRoadElement>(roadPrefab, roadParent.transform);
        _currentElementRX.Value = currentRoad;
        _currentElementRX.Value.IsPlayerInside = true;
        currentRoad.SetPosition(roadParent.transform.position);
        _roads.Add(currentRoad);
        for(int i = 0; i < countInMap-1; i++)
        {
            var currentRoadCircle = _container.InstantiatePrefabForComponent<IRoadElement>(roadPrefab, roadParent.transform);
            currentRoadCircle.SetPosition(positions[i]);
            currentRoadCircle.OnPlayerEnter += RebuildRoad;
            _roads.Add(currentRoadCircle);
        }
    }





    public void RebuildRoad(IRoadElement road)
    {
        RefreshRoad();
        _roadsInRightPos.Clear();
        _roadsInWrongPos.Clear();
        _currentElementRX.Value = road;
        var emptyPos = CheckEmptyPos();
        _roadsInWrongPos = _roads.Except(_roadsInRightPos).ToList();
        for(int i = 0; i < emptyPos.Count; i++)
        {
            _roadsInWrongPos[i].SetPosition(emptyPos[i]);
        }
    }

    private List<Vector3> CheckEmptyPos()
    {
        var emptyPosList = new List<Vector3>();
        _roadsInRightPos = new List<IRoadElement>();
        for(int i = 0; i < countInMap-1; i++)
        {
            if(IsEmptyPos(_currentElementRX.Value.Transform.position, positions[i]))
            {
                emptyPosList.Add(positions[i]+_currentElementRX.Value.Transform.position);      
            }
            else
            {
                _roadsInRightPos.Add(_roads.FirstOrDefault(x => x.Transform.position == positions[i] + _currentElementRX.Value.Transform.position));
            }
        }
        return emptyPosList;
    }

    private bool IsEmptyPos(Vector3 currentPos, Vector3 offsetPos)
    {
        var targetPos = currentPos + offsetPos;
        
        foreach(var road in _roads)
        {
            if(road.Transform.position == targetPos)
            {               
                return false;
            }
        }
        return true;
    }

    private void RefreshRoad()
    {
        foreach(var road in _roads)
        {
            road.RefreshCollider();
        }
    }

   

   

    void IDisposable.Dispose()
    {
        _disposables?.Dispose();
        if(_roads.Count > 0)
        {
            foreach(var road in _roads)
            {
                road.OnPlayerEnter -= RebuildRoad;
            }
        }
    }
}

public enum Direction
{
    left,
    right,
    up,
    down,
    none,
    upRight,
    upLeft,
    downRight,
    downLeft
}

