using System.Collections.Generic;
using System.Linq;

namespace Wemogy.Core.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] RemoveLast<T>(this T[] target)
        {
            return target.TakeAsArray(target.Length - 1);
        }

        public static T[] TakeAsArray<T>(this IEnumerable<T> target, int take)
        {
            return target.Take(take).ToArray();
        }

        public static string Join(this IEnumerable<object> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}
