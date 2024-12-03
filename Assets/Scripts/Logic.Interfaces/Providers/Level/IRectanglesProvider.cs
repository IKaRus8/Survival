using System.Collections.Generic;
using Logic.RuntimeData;
using Logic.RuntimeData.Rectangles;

namespace Logic.Interfaces.Providers.Level
{
    public interface IRectanglesProvider
    {
        HashSet<EnemyRectangle> GetEnemyRectangles();
        HashSet<EnemyRectangle> GetEnemyInRectangle(Rectangle rectangle);
        IAsyncEnumerable<EnemyRectangle[]> GetEnemiesByGridElements();
        HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle);
        
        HashSet<GridRectangle> GetGridRectangles();
        GridRectangle GetGridRectangleBy(int index);
        GridRectangle GetGridRectangleBy(Rectangle rectangle);
        
        Rectangle GetHeroRectangle();
    }
}