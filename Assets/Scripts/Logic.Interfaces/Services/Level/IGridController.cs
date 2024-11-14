using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface IGridController
    {
        IGridElement GetRandomGridPlaneWithOutPlayer();

        void ReplaceGrid(Vector3 centerPosition);
    }
}