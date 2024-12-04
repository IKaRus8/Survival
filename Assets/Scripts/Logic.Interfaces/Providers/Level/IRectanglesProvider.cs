using System.Collections.Generic;
using Logic.RuntimeData.Rectangles;

namespace Logic.Interfaces.Providers.Level
{
    public interface IRectanglesProvider
    {
        HashSet<EnemyRectangle> GetEnemyRectangles();
        HashSet<EnemyRectangle> GetEnemyInRectangle(Rectangle rectangle);
        IAsyncEnumerable<EnemyRectangle[]> GetEnemiesByGridElements();
        HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle);
        
        HashSet<Rectangle> GetGridRectangles();
        Rectangle GetGridRectangleBy(Rectangle rectangle);
        
        Rectangle GetHeroRectangle();
    }
}