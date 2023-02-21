using System.Collections;
using Wemogy.Core.Primitives;

namespace Wemogy.Core.Extensions
{
    public static class BitArrayExtensions
    {
        /// <summary>
        /// Performs a bitwise OR operation on the current BitArray and the specified Bits.
        /// The current BitArray will not be modified.
        /// </summary>
        /// <returns>The modified BitArray</returns>
        public static BitArray Or(this BitArray bitArray, Bits bits)
        {
            var result = new BitArray(bitArray.Length);

            for (int i = 0; i < bitArray.Length; i++)
            {
                var flagIndex = i + 1;
                var flag = bits.GetFlag(flagIndex);
                result.Set(i, bitArray.Get(i) || flag);
            }

            return result;
        }

        /// <summary>
        /// Performs a bitwise AND operation on the current BitArray and the specified Bits.
        /// The current BitArray will not be modified.
        /// </summary>
        /// <returns>The modified BitArray</returns>
        public static BitArray And(this BitArray bitArray, Bits bits)
        {
            var result = new BitArray(bitArray.Length);

            for (int i = 0; i < bitArray.Length; i++)
            {
                var flag = bits.GetFlag(i);
                result.Set(i, bitArray.Get(i) && flag);
            }

            return result;
        }

        /// <summary>
        /// Performs a bitwise Equal check on the current BitArray and the specified Bits.
        /// </summary>
        /// <returns>The modified BitArray</returns>
        public static bool HasEqualBits(this BitArray bitArray, Bits bits)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                var flag = bits.GetFlag(i);
                if (bitArray.Get(i) != flag)
                {
                    return false;
                }
            }

            return true;
        }

        public static Bits ToBits(this BitArray bitArray)
        {
            var bits = new Bits();

            for (int i = 0; i < bitArray.Length; i++)
            {
                var flagIndex = i + 1;
                if (bitArray.Get(i))
                {
                    bits.SetFlag(flagIndex);
                }
                else
                {
                    bits.RemoveFlag(flagIndex);
                }
            }

            return bits;
        }
    }
}
