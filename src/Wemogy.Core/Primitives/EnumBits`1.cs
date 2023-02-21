using System;
using System.Collections.Generic;
using System.Linq;

namespace Wemogy.Core.Primitives
{
    public class EnumBits<TEnum>
        where TEnum : Enum
    {
        public static EnumBits<TEnum> FromFlags(IEnumerable<TEnum> flags)
        {
            var enumBits = new EnumBits<TEnum>();

            var flagIndices = flags.Select(flag => (int)(object)flag);
            enumBits._bits.SetFlags(flagIndices);

            return enumBits;
        }

        public static EnumBits<TEnum> FromFlag(TEnum flags)
        {
            return EnumBits<TEnum>.FromFlags(new[] { flags });
        }

        public static EnumBits<TEnum> Empty => FromFlags(Array.Empty<TEnum>());

        private readonly Bits _bits;

        public int Length => _bits.Length;

        public EnumBits(Bits bits)
        {
            _bits = bits;
        }

        public EnumBits(string? base64UrlValue = null)
        {
            _bits = new Bits(base64UrlValue);
        }

        public EnumBits(TEnum flagIndex)
        {
            _bits = Bits.FromFlag((int)(object)flagIndex);
        }

        public EnumBits(IEnumerable<TEnum> flagIndices)
        {
            var enums = flagIndices.Select(x => (Enum)x);
            _bits = Bits.FromFlags(enums.ToArray());
        }

        public bool HasFlag(TEnum flag)
        {
            var flagIndex = (int)(object)flag;
            return _bits.HasFlag(flagIndex);
        }

        public bool HasFlags(params TEnum[] flags)
        {
            var flagIndices = flags.Select(flag => (int)(object)flag);
            return _bits.HasFlags(flagIndices);
        }

        public override string ToString()
        {
            return _bits.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is EnumBits<TEnum> enumBits)
            {
                return ToString().Equals(enumBits.ToString());
            }

            return this == obj;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
