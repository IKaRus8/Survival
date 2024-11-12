using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Перемешать массив
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)  
        {  
            var buffer = list.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = UnityEngine.Random.Range(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
        
        /// <summary>
        /// Перемешать массив
        /// </summary>
        public static IList<T> Shuffle<T>(this IList<T> list)  
        {  
            var buffer = list.ToList();
            var n = buffer.Count;  
            while (n > 1) {  
                n--;  
                var k = UnityEngine.Random.Range(0, n + 1);  
                (buffer[k], buffer[n]) = (buffer[n], buffer[k]);
            }

            return buffer;
        }
        
        /// <summary>
        /// Взять случайный элемент списка
        /// </summary>
        /// <param name="array">Массив</param>
        /// <typeparam name="TValue">Некий тип</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">Если список пуст</exception>
        public static TValue RandomElement<TValue>(this TValue[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        /// <summary>
        /// Взять случайный элемент списка
        /// </summary>
        /// <param name="list">Список</param>
        /// <typeparam name="TValue">Тип</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">Если список пуст</exception>
        public static TValue RandomElement<TValue>(this List<TValue> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        
        public static bool IsNullOrEmpty<T>(this T collection)
            where T : System.Collections.IEnumerable
        {
            if (collection == null)
            {
                return true;
            }

            foreach (var item in collection)
            {
                if (item != null)
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Перемешать список
        /// </summary>
        /// <param name="list">Список</param>
        /// <typeparam name="TValue">Некий тип</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">Если список пуст</exception>
        public static List<TValue> Shake<TValue>(this List<TValue> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            for (var i = 0; i < list.Count; i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, list.Count);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
            
            return list;
        }

        public static void AddUnique<TValue>(this IList<TValue> list, TValue value)
        {
            if (list == null)
            {
                list = new List<TValue>();
            }
            
            if (list.Contains(value))
            {
                return;
            }
            
            list.Add(value);
        }


    }
}