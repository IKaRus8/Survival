using Logic.RuntimeData.Rectangles;
using UnityEngine;

namespace Logic.Interfaces.Unity
{
    public interface IGridElement
    {
        int Index { get; set; }
        Vector3 Position { get; }
        Rectangle ElementRectangle { get; }
        
        void SetPosition(Vector3 position);
    }
}