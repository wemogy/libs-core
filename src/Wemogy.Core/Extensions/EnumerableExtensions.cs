using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wemogy.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<List<T>> Chunk<T>(this IEnumerable<T> enumerable, int chunkLength)
        {
            return enumerable
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkLength)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<TMapped> MapChunks<T, TMapped>(
            this IEnumerable<T> enumerable,
            int chunkLength,
            Func<List<T>, TMapped> mappingCallback)
        {
            var chunks = enumerable.Chunk(chunkLength);

            var mappedChunks = chunks.Select(mappingCallback).ToList();

            return mappedChunks;
        }

        public static string ToBitString(this IEnumerable<bool> bits)
        {
            var bitsChars = bits.Select(bit => bit ? '1' : '0').ToList();

            // reverse because the first bit must be on the right side
            bitsChars.Reverse();
            var bitsString = new string(bitsChars.ToArray());
            return bitsString;
        }

        public static object ToListOfType(this IEnumerable<object> value, Type type)
        {
            var list = (IList)Activator.CreateInstance(type);
            foreach (var item in value)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
