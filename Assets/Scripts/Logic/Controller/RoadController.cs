using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GridController: IDisposable, IGridController
{
    private const float Offset = 10;

    private readonly List<IGridElement> _roads = new();
    private readonly List<IGridElement> _roadsInRightPos = new();
    private readonly List<Vector3> _positions = new()
    {
            new Vector3(0, 0, 0),
            new Vector3(-Offset, 0, 0),
            new Vector3(Offset, 0, 0),
            new Vector3(0, 0, Offset),
            new Vector3(0, 0, -Offset),
            new Vector3(Offset, 0, Offset),
            new Vector3(-Offset, 0, -Offset),
            new Vector3(Offset, 0, -Offset),
            new Vector3(-Offset, 0, Offset)
         };

    private IInstantiator _container;
    private IAssetService _assetService;
    private ISceneObjectContainer _objectContainer;

    private readonly ReactiveProperty<IGridElement> _currentElementRx = new();

    private Transform _playerTransform;
    private CompositeDisposable _disposables;
    private int _countInMap = 9;


    public GridController(IInstantiator installer, IAssetService assetService, ISceneObjectContainer objectContainer)
    {
        _container = installer;
        _assetService = assetService;   
        _objectContainer = objectContainer;
        _disposables = new();
        Init();        
    }   

    public void Init()
    {        
        CreateStartField();
    }  

    private async void CreateStartField()
    {
        var roadParent = _objectContainer.RoadParent;     
        roadParent.transform.position = Vector3.zero;
        var roadPrefab = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Plane.prefab");
        CreateLevelGrid(roadPrefab, roadParent.transform);       
    }

    private  void CreateLevelGrid(GameObject roadPrefab, Transform roadParent)
    {      
        for(int i = 0; i < _countInMap; i++)
        {
            var currentRoadGrid = _container.InstantiatePrefabForComponent<IGridElement>(roadPrefab, roadParent.transform);
            currentRoadGrid.SetPosition(_positions[i]);
            currentRoadGrid.OnPlayerEnter += RebuildRoad;
            if (i == 0)
            {
                _currentElementRx.Value = currentRoadGrid;
            }           
            _roadsInRightPos.Add(currentRoadGrid);            
            _roads.Add(currentRoadGrid);
        }        
    }

    public void RebuildRoad(IGridElement grid)
    {        
        _roadsInRightPos.Clear();
        _currentElementRx.Value.Reset();
        _currentElementRx.Value = grid;        

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
        var centerPosition = _currentElementRx.Value.Transform.position;

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

    public List<IGridElement> GetRoadsForSpawn()
    {
        return _roadsInRightPos.Where(x => x.IsPlayerInside == false).ToList();
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

public interface IGridController
{
    List<IGridElement> GetRoadsForSpawn();
}


