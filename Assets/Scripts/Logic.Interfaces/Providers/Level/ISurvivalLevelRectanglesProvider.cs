using System.Collections.Generic;
using System.Threading;
using Logic.RuntimeData.Rectangles;

namespace Logic.Interfaces.Providers.Level
{
    public interface ISurvivalLevelRectanglesProvider : IRectanglesProvider
    {
        IAsyncEnumerable<EnemyRectangle[]> GetEnemiesByGridElements(CancellationToken cancellationToken);
        
        HashSet<Rectangle> GetGridRectangles();
        Rectangle GetGridRectangleBy(Rectangle rectangle);
        
    }
}