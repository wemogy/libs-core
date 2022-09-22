using System;

namespace Wemogy.Core.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            return Convert.ToInt32(enumValue);
        }

        /// <summary>
        /// Converts an enum value into a long value
        /// Only works for long enums!
        /// enum MyEnum: long { ... }
        /// </summary>
        public static long ToLong<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            return Convert.ToInt64(enumValue);
        }

        /// <summary>
        /// Converts an enum value into a ulong value
        /// Only works for ulong enums!
        /// enum MyEnum: ulong { ... }
        /// </summary>
        public static ulong ToULong<TEnum>(this TEnum enumValue)
            where TEnum : Enum
        {
            return Convert.ToUInt64(enumValue);
        }
    }
}
