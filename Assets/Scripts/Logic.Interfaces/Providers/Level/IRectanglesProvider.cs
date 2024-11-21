using System.Collections.Generic;
using Logic.RuntimeData;

namespace Logic.Interfaces.Providers.Level
{
    public interface IRectanglesProvider
    {
        IReadOnlyCollection<EnemyRectangle> GetEnemyRectangles();
        
        IReadOnlyCollection<GridRectangle> GetGridRectangles();
        GridRectangle GetGridRectangle(int index);
        
        Rectangle GetHeroRectangle();
    }
}