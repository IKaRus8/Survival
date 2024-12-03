using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using Logic.RuntimeData.Rectangles;
using UnityEngine;

namespace Logic.Unity.Grid
{
    public class GridElement : MonoBehaviour, IGridElement
    {
        private Transform _transform;
        private float _radius;

        public Vector3 Position => _transform.position;
        public int Index { get; set; }
        public GridRectangle ElementRectangle => new(Index, Position, _radius);

        private void Awake()
        {
            _transform = transform;
            
            var render = GetComponent<Renderer>();
            // Определяем размеры объекта через его границы
            _radius = render.bounds.size.x * 0.5f;
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}