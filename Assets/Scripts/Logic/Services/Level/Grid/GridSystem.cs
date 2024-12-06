using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
        private const float Offset = 5f;
        private const int GridSize = 10;
        private const int HalfGridSize = GridSize / 2;
        private const float ViewDistance = HalfGridSize * Offset;

        private readonly List<IGridElement> _grid;
        private readonly IAssetService _assetService;
        private readonly CompositeDisposable _disposables;
        private readonly Transform _parentTransform;

        private Transform _playerTransform;
        private GridElement _gridElementPrefab;
        private Vector3 _centerPosition;

        public IReadOnlyCollection<IGridElement> Grid => _grid;

        public GridSystem(
            IAssetService assetService,
            ILevelSceneObjectContainer objectContainer)
        {
            _assetService = assetService;
            _parentTransform = objectContainer.GridParent.transform;

            _disposables = new CompositeDisposable();
            _grid = new List<IGridElement>();

            CreateStartField().Forget();
        }

        public IGridElement GetRandomGridPlaneWithOutHero()
        {
            return _grid.Shake().FirstOrDefault(g => g.Position != _centerPosition);
        }

        public void ReplaceGridAround(Vector3 position)
        {
            if (_centerPosition == position)
            {
                return;
            }
            
            CreateLevelGrid(position).Forget();
        }

        public IEnumerator<IGridElement> GetEnumerator()
        {
            foreach (var element in Grid)
            {
                yield return element;
            }
        }

        private async UniTaskVoid CreateStartField()
        {
            _gridElementPrefab = await _assetService.LoadWithComponent<GridElement>(GridPlaneKey);

            CreateLevelGrid(Vector3.zero).Forget();
        }

        private async UniTask CreateLevelGrid(Vector3 startPosition)
        {
            _centerPosition = startPosition;

            var offsetList = GenerateGridOffsets();

            foreach (var offset in offsetList)
            {
                var isEmpty = IsEmptyPosition(offset);

                if (!isEmpty)
                {
                    continue;
                }

                var newGrid = await GetGridElement();

                newGrid.SetPosition(offset);

                _grid.Add(newGrid);
            }
        }

        private async UniTask<IGridElement> GetGridElement()
        {
            var newGridElement = GetWrongGridElement();

            if (newGridElement != null)
            {
                return newGridElement;
            }

            newGridElement = await CreateGridElement();

            return newGridElement;
        }

        private async UniTask<IGridElement> CreateGridElement()
        {
            var elements = await Object.InstantiateAsync(_gridElementPrefab, _parentTransform);

            if (elements.IsNullOrEmpty())
            {
                Debug.LogError("Could not instantiate grid element");

                return null;
            }

            var newGrid = elements[0];

            return newGrid;
        }

        private bool IsEmptyPosition(Vector3 targetPosition)
        {
            foreach (var gridElement in this)
            {
                if (gridElement.Position == targetPosition)
                {
                    return false;
                }
            }

            return true;
        }

        private IGridElement GetWrongGridElement()
        {
            foreach (var gridElement in this)
            {
                var distance = Vector3.Distance(_centerPosition, gridElement.Position);

                if (distance > ViewDistance)
                {
                    return gridElement;
                }
            }

            return null;
        }

        private List<Vector3> GenerateGridOffsets()
        {
            var offsetList = new List<Vector3>();

            for (var x = 0; x < GridSize; x++)
            {
                for (var z = 0; z < GridSize; z++)
                {
                    var xOffset = (x - HalfGridSize) * Offset;
                    var zOffset = (z - HalfGridSize) * Offset;
                    
                    var offset = _centerPosition + new Vector3(xOffset, 0, zOffset);

                    offsetList.Add(offset);
                }
            }

            return offsetList;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}