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
            // Проверка по оси X
            if (another.MaxPoint.x < rectangle.MinPoint.x 
                || another.MinPoint.x > rectangle.MaxPoint.x)
            {
                return false;
            }

            // Проверка по оси Z
            if (another.MaxPoint.z < rectangle.MinPoint.z 
                || another.MinPoint.z > rectangle.MaxPoint.z)
            {
                return false;
            }

            return true;
        }
    }
}