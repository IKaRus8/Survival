using System.Collections.Generic;
using Logic.RuntimeData.Rectangles;

namespace Logic.Interfaces.Providers.Level
{
    public interface IRectanglesProvider
    {
        HashSet<EnemyRectangle> GetEnemyRectangles();
        HashSet<EnemyRectangle> GetEnemyInRectangle(Rectangle rectangle);
        HashSet<EnemyRectangle> GetNearestEnemyRectangles(Rectangle rectangle);
        
        Rectangle GetHeroRectangle();
    }
}