using Logic.Interfaces.Unity;
using Logic.RuntimeData;
using UnityEngine;

namespace Logic.Unity.Grid
{
    public class GridElement : MonoBehaviour, IGridElement
    {
        [SerializeField]
        private Transform _transform;

        private Vector3 _size;

        public Transform Transform => _transform;
        public Rectangle ElementRectangle { get; private set; }
        public int Index { get; set; }

        private void Awake()
        {
            var render = GetComponent<Renderer>();
            // Определяем размеры объекта через его границы
            _size = render.bounds.size;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            
            ElementRectangle = new Rectangle(position, _size.x * 0.5f);
        }
    }
}