using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Utilities.Extensions
{
    public static class UnityExtensions
    {
        [ContractAnnotation("nullable:null => halt")]
        public static void CheckNotNull<T>(
            [CanBeNull] this T nullable,
            [CallerMemberName] string objName = "")
            where T : class
        {
            if (nullable == null)
            {
                throw CreateNullException(objName);
            }
        }

        [ContractAnnotation("collection:null => halt")]
        public static void CheckSelfAndItemsNotNullOrEmpty<T>(
            [CanBeNull] this T collection,
            [CallerMemberName] string objName = "")
            where T : System.Collections.ICollection
        {
            if (collection == null)
            {
                throw CreateNullException(objName);
            }

            if (collection.Count == 0)
            {
                throw CreateNullException(objName);
            }

            foreach (var item in collection)
            {
                if (item == null)
                {
                    throw CreateNullException(objName);
                }
            }
        }

        public static T ThrowIfNull<T>(
            [CanBeNull] this T obj,
            [CallerMemberName] string objName = "")
        {
            if (obj == null)
            {
                throw CreateNullException($"{objName}");
            }

            return obj;
        }

        private static Exception CreateNullException(string memberName)
        {
            return new InvalidOperationException($"Expected nullable (in {memberName} method) to have value");
        }

        public static Image SetAlpha(
            this Image img,
            float a)
        {
            var color = img.color;

            img.color = new Color(color.r, color.g, color.b, a);

            return img;
        }
        
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

    }
}