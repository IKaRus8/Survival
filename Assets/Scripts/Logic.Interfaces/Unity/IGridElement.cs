using Logic.RuntimeData;
using Logic.RuntimeData.Rectangles;
using UnityEngine;

namespace Logic.Interfaces.Unity
{
    public interface IGridElement
    {
        int Index { get; set; }
        Vector3 Position { get; }
        GridRectangle ElementRectangle { get; }
        
        void SetPosition(Vector3 position);
    }
}