using System.Collections.Generic;
using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Services.Level
{
    public interface IGridSystem
    {
        IReadOnlyCollection<IGridElement> Grid { get; }
        
        IGridElement GetRandomGridPlaneWithOutPlayer();

        void ReplaceGridAround(int index);
    }
}