using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using R3;
using UnityEngine;
using Zenject;

namespace Logic.Services
{
    public class GridSystem : IDisposable, IGridController
    {
        private const float Offset = 10;
        private const int CountInMap = 9;

        private readonly List<IGridElement> _roads;
        private readonly List<IGridElement> _roadsInRightPos;
        private readonly IInstantiator _container;
        private readonly IAssetService _assetService;
        private readonly ISceneObjectContainer _objectContainer;
        private readonly ReactiveProperty<IGridElement> _currentElementRx;
        private readonly CompositeDisposable _disposables;

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

        private Transform _playerTransform;

        public GridSystem(
            IInstantiator installer,
            IAssetService assetService,
            ISceneObjectContainer objectContainer)
        {
            _container = installer;
            _assetService = assetService;
            _objectContainer = objectContainer;

            _disposables = new CompositeDisposable();
            _roads = new List<IGridElement>();
            _roadsInRightPos = new List<IGridElement>();
            _currentElementRx = new ReactiveProperty<IGridElement>();

            CreateStartField().Forget();
        }

        public List<IGridElement> GetRoadsForSpawn()
        {
            return _roadsInRightPos.Where(x => x.IsPlayerInside == false).ToList();
        }

        private async UniTaskVoid CreateStartField()
        {
            var roadParent = _objectContainer.RoadParent;
            roadParent.transform.position = Vector3.zero;

            //TODO: ключ должен быть просто по имени: Plane
            var roadPrefab = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Plane.prefab");

            CreateLevelGrid(roadPrefab, roadParent.transform);
        }

        private void CreateLevelGrid(GameObject roadPrefab, Transform roadParent)
        {
            for (var i = 0; i < CountInMap; i++)
            {
                var currentRoadGrid =
                    _container.InstantiatePrefabForComponent<IGridElement>(roadPrefab, roadParent.transform);

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

        private void RebuildRoad(IGridElement grid)
        {
            _roadsInRightPos.Clear();
            _currentElementRx.Value.Reset();
            _currentElementRx.Value = grid;

            var emptyPos = CheckEmptyPos();

            var roadsInWrongPos = _roads.Except(_roadsInRightPos).ToList();

            for (var i = 0; i < emptyPos.Count; i++)
            {
                roadsInWrongPos[i].SetPosition(emptyPos[i]);
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

            foreach (var road in _roads)
            {
                if (road.Transform.position == targetPos)
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            _disposables?.Dispose();

            foreach (var road in _roads)
            {
                road.OnPlayerEnter -= RebuildRoad;
            }
        }
    }

//TODO: отдельный файл
    public interface IGridController
    {
        List<IGridElement> GetRoadsForSpawn();
    }
}