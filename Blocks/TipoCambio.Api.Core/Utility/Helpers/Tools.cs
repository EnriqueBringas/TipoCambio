using System;
using System.Collections.Generic;
using System.Linq;

namespace TipoCambio.Api.Core.Utility.Helpers
{
    public static class Tools
    {
        public static bool IsNullOrEmpty<T>(this T value)
        {
            if (typeof(T) == typeof(string))
                return string.IsNullOrEmpty(value as string);

            return value == null || value.Equals(default(T));
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        public static void UnionList<T>(ref List<T> elements, List<T> items)
        {
            if (elements == null)
                elements = new List<T>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    elements.Add(item);
                }
            }
        }

        public static void AddToArray<T>(ref T[] elements, T item)
        {
            var list = elements == null ? new List<T>() : elements.ToList();

            list.Add(item);

            elements = list.ToArray();
        }

        public static void AddToArray<T>(ref T[] elements, T[] items)
        {
            var list = elements == null ? new List<T>() : elements.ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    list.Add(item);
                } 
            }

            elements = list.ToArray();
        }
    }
}