using System;
using System.Collections.Generic;
using System.Linq;

namespace Wemogy.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> GetAllCombinations<T>(this List<T> list)
        {
            var comboCount = (int)Math.Pow(
                2,
                list.Count) - 1;
            var result = new List<List<T>>();
            for (var i = 1; i < comboCount + 1; i++)
            {
                // make each combo here
                result.Add(new List<T>());
                for (var j = 0; j < list.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                    {
                        result.Last().Add(list[j]);
                    }
                }
            }

            // sort
            result = result.OrderBy(x => x.Count).ToList();
            return result;
        }

        public static void Pad<T>(this List<T?> list, int multiple, T? fillItem = default)
        {
            while (list.Count % multiple != 0)
            {
                list.Add(fillItem);
            }
        }

        public static void RemoveLast<T>(this List<T> list)
        {
            if (!list.Any())
            {
                return;
            }

            list.RemoveAt(list.Count - 1);
        }
    }
}
