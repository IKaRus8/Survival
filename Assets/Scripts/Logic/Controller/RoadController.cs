using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class RoadController: IDisposable
{
    private readonly List<IRoadElement> _roads = new(); 
    private List<IRoadElement> _roadsInRightPos = new();   
    private List<IRoadElement> _roadsInWrongPos = new();
    
    private IInstantiator _container;
    private IAssetService _assetService;   

    private ReactiveProperty <IRoadElement> _currentElementRX= new ReactiveProperty<IRoadElement>();
    private float offset=10;    
    private Transform _playerTransform;
    private CompositeDisposable _disposables;
    private int countInMap = 9;
    private List<Vector3> positions;

    public RoadController(IInstantiator installer, IAssetService assetService)
    {
        _container = installer;
        _assetService = assetService;       
        _disposables = new();
        Init();        
    }   

    public void Init()
    {
        CreatePositions();
        CreateStartField();
    }

    private void CreatePositions()
    {
         positions = new List<Vector3>
         {
            new Vector3(0, 0, 0),
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
        BuildStartRoad(roadPrefab, roadParent.transform);       
    }

    private  void BuildStartRoad(GameObject roadPrefab, Transform roadParent)
    {      
        for(int i = 0; i < countInMap; i++)
        {
            var currentRoadCircle = _container.InstantiatePrefabForComponent<IRoadElement>(roadPrefab, roadParent.transform);
            currentRoadCircle.SetPosition(positions[i]);
            currentRoadCircle.OnPlayerEnter += RebuildRoad;
            if (i == 0)
            {
                _currentElementRX.Value = currentRoadCircle;
            }
            _roads.Add(currentRoadCircle);
        }
        
    }

    public void RebuildRoad(IRoadElement road)
    {        
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
        for(int i = 0; i < countInMap; i++)
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
  
    void IDisposable.Dispose()
    {
        _disposables?.Dispose();

        foreach (var road in _roads)
        {
            road.OnPlayerEnter -= RebuildRoad;
        }
    }
}


