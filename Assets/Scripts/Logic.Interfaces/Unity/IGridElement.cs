using Logic.RuntimeData;
using UnityEngine;

namespace Logic.Interfaces.Unity
{
    public interface IGridElement
    {
        int Index { get; set; }
        Transform Transform { get; }
        Rectangle ElementRectangle { get; }
        
        void SetPosition(Vector3 position);
    }
}