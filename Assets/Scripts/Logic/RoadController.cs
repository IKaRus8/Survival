using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class RoadController: IDisposable
{
    private const float _offset = 10;

    private readonly List<IRoadElement> _roads = new();
    private readonly List<IRoadElement> _roadsInRightPos = new();
    private readonly List<Vector3> _positions = new List<Vector3>
         {
            new Vector3(0, 0, 0),
            new Vector3(-_offset, 0, 0),
            new Vector3(_offset, 0, 0),
            new Vector3(0, 0, _offset),
            new Vector3(0, 0, -_offset),
            new Vector3(_offset, 0, _offset),
            new Vector3(-_offset, 0, -_offset),
            new Vector3(_offset, 0, -_offset),
            new Vector3(-_offset, 0, _offset)
         };

    private IInstantiator _container;
    private IAssetService _assetService;

    private ReactiveProperty<IRoadElement> _currentElementRX = new ReactiveProperty<IRoadElement>();

    private Transform _playerTransform;
    private CompositeDisposable _disposables;
    private int _countInMap = 9;


    public RoadController(IInstantiator installer, IAssetService assetService)
    {
        _container = installer;
        _assetService = assetService;       
        _disposables = new();
        Init();        
    }   

    public void Init()
    {        
        CreateStartField();
    }

   

    private async void CreateStartField()
    {
        var roadParent = _container.CreateEmptyGameObject("Road");       
        roadParent.transform.position = Vector3.zero;
        var roadPrefab = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Plane.prefab");
        CreateLevelGrid(roadPrefab, roadParent.transform);       
    }

    private  void CreateLevelGrid(GameObject roadPrefab, Transform roadParent)
    {      
        for(int i = 0; i < _countInMap; i++)
        {
            var currentRoadGrid = _container.InstantiatePrefabForComponent<IRoadElement>(roadPrefab, roadParent.transform);
            currentRoadGrid.SetPosition(_positions[i]);
            currentRoadGrid.OnPlayerEnter += RebuildRoad;
            if (i == 0)
            {
                _currentElementRX.Value = currentRoadGrid;
            }
            _roads.Add(currentRoadGrid);
        }
        
    }

    public void RebuildRoad(IRoadElement road)
    {        
        _roadsInRightPos.Clear();       
        _currentElementRX.Value = road;

        var emptyPos = CheckEmptyPos();
        var _roadsInWrongPos = _roads.Except(_roadsInRightPos).ToList();

        for (int i = 0; i < emptyPos.Count; i++)
        {
            _roadsInWrongPos[i].SetPosition(emptyPos[i]);
        }
    }

    private List<Vector3> CheckEmptyPos()
    {
        var emptyPosList = new List<Vector3>();
        var centerPosition = _currentElementRX.Value.Transform.position;

        foreach (var pos in _positions)
        {
            if (IsEmptyPos(centerPosition, pos))
            {
                emptyPosList.Add(pos + centerPosition);
            }
            else
            {
                _roadsInRightPos.Add(_roads.FirstOrDefault(x => x.Transform.position == pos + centerPosition));
            }
        }
        return emptyPosList;
    }

    private bool IsEmptyPos(Vector3 centerPos, Vector3 offsetPos)
    {
        var targetPos = centerPos + offsetPos;
        
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


