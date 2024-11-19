using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Unity;
using Logic.Unity.Grid;
using R3;
using UnityEngine;
using Utilities.Extensions;
using Object = UnityEngine.Object;

namespace Logic.Services.Level.Grid
{
    public class GridSystem : IGridSystem, IDisposable
    {
        private const string GridPlaneKey = "grid_plane";
        private const float Offset = 10;
        private const int CountInMap = 9;

        private readonly List<IGridElement> _grid;
        private readonly IAssetService _assetService;
        private readonly CompositeDisposable _disposables;
        private readonly Transform _parentTransform;

        private readonly List<Vector3> _offsetList = new()
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
        private int _centeredIndex;

        public IReadOnlyCollection<IGridElement> Grid => _grid;

        public GridSystem(
            IAssetService assetService,
            ILevelSceneObjectContainer objectContainer)
        {
            _assetService = assetService;
            _parentTransform = objectContainer.GridParent.transform;
            
            _disposables = new CompositeDisposable();
            _grid = new List<IGridElement>();
            _centeredIndex = 0;
            
            CreateStartField().Forget();
        }

        public IGridElement GetRandomGridPlaneWithOutPlayer()
        {
            var result = _grid.Shake().FirstOrDefault(g => g.Index != _centeredIndex);

            return result;
        }

        public void ReplaceGridAround(int index)
        {
            if (_centeredIndex == index)
            {
                return;
            }
            
            var centerGridElement = _grid.FirstOrDefault(g => g.Index == index);

            if (centerGridElement == null)
            {
#if UNITY_EDITOR || DEBUG
                Debug.LogWarning($"Grid {index} does not exist");
#endif
                return;
            }
            
            _centeredIndex = index;
            var centerPosition = centerGridElement.Transform.position;
            
            var emptyPos = GetEmptyPos(centerPosition);
            var gridElementInWrongPos = GetWrongGridElements(centerPosition);

            for (var i = 0; i < emptyPos.Count; i++)
            {
                gridElementInWrongPos[i].SetPosition(emptyPos[i]);
            }
        }

        private async UniTaskVoid CreateStartField()
        {
            _parentTransform.position = Vector3.zero;
            
            var roadPrefab = await _assetService.LoadWithComponent<GridElement>(GridPlaneKey);

            CreateLevelGrid(roadPrefab, _parentTransform).Forget();
        }

        private async UniTask CreateLevelGrid(GridElement gridPlanePrefab, Transform gridParent)
        {
            for (var i = 0; i < CountInMap; i++)
            {
                var elements = await Object.InstantiateAsync(gridPlanePrefab, gridParent.transform);

                if (elements.IsNullOrEmpty())
                {
                    Debug.LogError("Could not instantiate grid element");
                    
                    return;
                }

                var currentGrid = elements[0];

                currentGrid.Index = i;
                currentGrid.SetPosition(_offsetList[i]);

                _grid.Add(currentGrid);
            }
        }

        private List<Vector3> GetEmptyPos(Vector3 centerPosition)
        {
            var emptyPosList = new List<Vector3>();

            foreach (var pos in _offsetList)
            {
                if (IsEmptyPosition(centerPosition, pos))
                {
                    emptyPosList.Add(pos + centerPosition);
                }
            }

            return emptyPosList;
        }

        private bool IsEmptyPosition(Vector3 centerPos, Vector3 offsetPos)
        {
            var targetPos = centerPos + offsetPos;

            foreach (var gridElement in _grid)
            {
                if (gridElement.Transform.position == targetPos)
                {
                    return false;
                }
            }

            return true;
        }

        private List<IGridElement> GetWrongGridElements(Vector3 centerPosition)
        {
            var wrongGridPlane = new List<IGridElement>();
            
            foreach (var gridElement in _grid.Where(g => g.Index != _centeredIndex))
            {
                var distance = Vector3.Distance(centerPosition, gridElement.Transform.position);

                if (distance > Offset * 1.5f
                    || distance < Offset)
                {
                    wrongGridPlane.Add(gridElement);
                }
            }

            return wrongGridPlane;
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}