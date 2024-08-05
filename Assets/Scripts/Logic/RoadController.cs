using R3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Zenject;

public class RoadController: IDisposable
{
    private List<IRoadElement> _roads = new();
    private ObservableCollection <IRoadElement> _roadsObservable;
    private int countInRound = 5;
    private IInstantiator _container;
    private IAssetService _assetService;   
    private IRoadElement _currentElement;
    private float sizeElement=10;
    private List<IRoadElement> tempList=new List<IRoadElement>();
    private Transform _playerTransform;
    private CompositeDisposable _disposables;
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
        CreateStartField();
    }

    private async void CreateStartField()
    {
        var round = countInRound;
        var column = countInRound;

        var roadParent = _container.CreateEmptyGameObject("Road");       
        var roadPrefab = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Plane.prefab");

        for (var i = 0; i < countInRound; i++)
        {
            for (var j = 0; j < countInRound; j++)
            {
                var currentRoad = _container.InstantiatePrefabForComponent<IRoadElement>(roadPrefab, roadParent.transform);
                currentRoad.SetIndex(new Vector2Int(i, j));
                Debug.Log(currentRoad.Index);
                currentRoad.SetPosition(new Vector3(i*sizeElement, 0, j * sizeElement));
                currentRoad.OnPlayerEnter += RebuildRoad;
                _roads.Add(currentRoad);
            }
        }
        Debug.Log(_roads.Count);
        var centrElement = new Vector3(countInRound / 2, 0, countInRound / 2);
        var centr = new Vector3(-countInRound * sizeElement / 2+sizeElement/2, 0, -countInRound * sizeElement / 2+sizeElement/2);

        roadParent.transform.position = centr;    
        _currentElement = _roads.FirstOrDefault( x => x.Index.x == centrElement.x && x.Index.y == centrElement.z );
        _currentElement.IsPlayerInside = true;
    }


    public void RebuildRoad(IRoadElement road)
    {
        _currentElement.RefreshCollider();
        var dir = CheckDirectrion(road.Index);     
    
        switch (dir)
        {
            case Direction.left:
                MoveElementsLeft();
                //UpdateIndices(Direction.left, tempList);
                break;
            case Direction.right:
               // MoveElementsRight(tempList);
               // UpdateIndices(Direction.right, tempList);
                break;
            case Direction.up:
              // MoveElementsUp(tempList);
              //  UpdateIndices(Direction.up, tempList);
                break;
            case Direction.down:
              //  MoveElementsDown(tempList);
             //   UpdateIndices(Direction.down, tempList);
                break;
        }
        _currentElement=road;
    }


    public void MoveElementsLeft()
    {

        tempList.Clear();
        tempList = new List<IRoadElement>();
        tempList = _roads.Where(road => road.Index.x == countInRound - 1).ToList();
        _roads = _roads.Where(road => road.Index.x != countInRound - 1).ToList();
        Debug.Log(tempList.Count);
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].Index = new Vector2Int(0, tempList[i].Index.y);
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].SetPosition(new Vector3(tempList[i].Transform.position.x - countInRound * sizeElement, 0, tempList[i].Transform.position.z));
        }

        //update indexes

        for(int i = 0; i < _roads.Count; i++)
        {
            _roads[i].SetIndex(new Vector2Int(_roads[i].Index.x + 1, _roads[i].Index.y));
        }
       
    }


   
  

   
    private Direction CheckDirectrion(Vector2Int index)
    {
        var dir = Direction.none;
        if (index.x > _currentElement.Index.x&& index.y == _currentElement.Index.y)
        {
           dir = Direction.right;
        }
        else if(index.x < _currentElement.Index.x && index.y == _currentElement.Index.y)
        {
            dir = Direction.left;
        }
        else if (index.x == _currentElement.Index.x && index.y > _currentElement.Index.y)
        {
            dir = Direction.up;
        }
        else if (index.x == _currentElement.Index.x && index.y < _currentElement.Index.y)
        {
            dir = Direction.down;
        }    
        else if(index.x > _currentElement.Index.x && index.y > _currentElement.Index.y)
        {
            dir = Direction.upRight;
        }
        else if (index.x < _currentElement.Index.x && index.y > _currentElement.Index.y)
        {
            dir = Direction.upLeft;
        }
        else if (index.x > _currentElement.Index.x && index.y < _currentElement.Index.y)
        {
            dir = Direction.downRight;
        }
        else if (index.x < _currentElement.Index.x && index.y < _currentElement.Index.y)
        {
            dir = Direction.downLeft;
        }
        
        Debug.Log(dir);
        return dir;

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

