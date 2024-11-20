using System.Collections;
using System.Collections.Generic;
using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Services.Level
{
    public interface IGridSystem : IEnumerable
    {
        IGridElement this[int index] { get; }
        
        IReadOnlyCollection<IGridElement> Grid { get; }
        
        IGridElement GetRandomGridPlaneWithOutHero();

        void ReplaceGridAround(int index);
    }
}