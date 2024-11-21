#if UNITY_EDITOR

using System.Collections.Generic;
using Logic.RuntimeData;
using UnityEditor;
using UnityEngine;

namespace Logic.Services
{
    public class DebugRectangleDrawer : MonoBehaviour
    {
        private static readonly List<(Rectangle rectangle, Color color)> _rectangles = new();
        private static readonly List<(Vector3 position, int number)> _numbers = new();
        private static readonly List<(Vector3, Vector3, Color color)> _lines = new();

        /// <summary>
        /// Добавляет прямоугольник для отрисовки.
        /// </summary>
        /// <param name="rectangle">Прямоугольник для отрисовки.</param>
        public static void AddRectangle(Rectangle rectangle)
        {
            var randomColor = new Color(Random.value, Random.value, Random.value);
            _rectangles.Add((rectangle, randomColor));
        }

        public static void AddLine(Vector3 start, Vector3 end)
        {
            _lines.Add((start, end, RandomHelper.GetRandomColor()));
        }

        /// <summary>
        /// Очищает список прямоугольников для отрисовки.
        /// </summary>
        public static void Clear()
        {
            _rectangles.Clear();
            _numbers.Clear();
            _lines.Clear();
        }

        private void OnDrawGizmos()
        {
            foreach (var (rectangle, color) in _rectangles)
            {
                DrawRectangle(rectangle, color);
            }
            
            foreach (var (position, number) in _numbers)
            {
                DrawNumber(position, number);
            }

            foreach (var line in _lines)
            {
                DrawLine(line.Item1, line.Item2, line.Item3);
            }
        }

        private void DrawRectangle(Rectangle rect, Color color)
        {
            Gizmos.color = color;

            var min = rect.MinPoint;
            var max = rect.MaxPoint;

            Vector3 topLeft = new Vector3(min.x, 0, max.z);
            Vector3 topRight = new Vector3(max.x, 0, max.z);
            Vector3 bottomRight = new Vector3(max.x, 0, min.z);
            Vector3 bottomLeft = new Vector3(min.x, 0, min.z);

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }
        
        /// <summary>
        /// Добавляет число для отрисовки в указанной позиции.
        /// </summary>
        /// <param name="position">Позиция в плоскости XZ.</param>
        /// <param name="number">Число для отрисовки.</param>
        public static void AddNumber(Vector3 position, int number)
        {
            _numbers.Add((position, number));
        }
        
        private void DrawNumber(Vector3 position, int number)
        {
            var style = new GUIStyle
            {
                fontSize = 20,
                normal = new GUIStyleState { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter
            };

            // Позиция экрана для текста
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

            if (screenPosition.z > 0) // Проверяем, находится ли объект перед камерой
            {
                Vector2 textPosition = new Vector2(screenPosition.x, Screen.height - screenPosition.y);
                Handles.Label(position, number.ToString(), style);
            }
        }

        private void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Gizmos.color = color;

            Gizmos.DrawLine(start, end);
        }
    }
}

#endif