using Logic.RuntimeData;

namespace Utilities.Extensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// Проверка пересечения прямоугольников
        /// </summary>
        public static bool IsIntersection(this Rectangle rectangle, Rectangle another)
        {
            if (another.MaxPoint.x < rectangle.MinPoint.x)
            {
                return false;
            }

            if (another.MaxPoint.y < rectangle.MinPoint.y)
            {
                return false;
            }

            if (another.MinPoint.x > rectangle.MaxPoint.x)
            {
                return false;
            }

            if (another.MinPoint.y > rectangle.MaxPoint.y)
            {
                return false;
            }

            return true;
        }
    }
}