using System.Collections;
using System.Collections.Generic;
using Logic.Interfaces.Unity;
using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface IGridSystem : IEnumerable
    {
        IReadOnlyCollection<IGridElement> Grid { get; }
        
        IGridElement GetRandomGridPlaneWithOutHero();

        void ReplaceGridAround(Vector3 position);
    }
}