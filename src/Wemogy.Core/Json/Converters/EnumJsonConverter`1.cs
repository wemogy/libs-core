using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wemogy.Core.Json.Converters
{
    /// <summary>
    /// This JSON converter accepts string and int values in a JSON string and serializes enum values as int always
    /// </summary>
    public class EnumJsonConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        private readonly TypeCode _enumTypeCode;

        public EnumJsonConverter()
        {
            _enumTypeCode = Type.GetTypeCode(typeof(TEnum));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        // Thanks to: https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/EnumConverter.cs
        private TEnum ReadAsPropertyNameCore(ref Utf8JsonReader reader)
        {
            var enumString = reader.GetString();

            // Try parsing case sensitive first
            if (!Enum.TryParse(enumString, out TEnum value)
                && !Enum.TryParse(enumString, ignoreCase: true, out value))
            {
                throw new JsonException($"Can not parse enum value '{enumString}' to enum {typeof(TEnum).FullName}");
            }

            return value;
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType token = reader.TokenType;

            if (token == JsonTokenType.String)
            {
                return ReadAsPropertyNameCore(ref reader);
            }

            switch (_enumTypeCode)
            {
                // Switch cases ordered by expected frequency

                case TypeCode.Int32:
                    if (reader.TryGetInt32(out int int32))
                    {
                        return Unsafe.As<int, TEnum>(ref int32);
                    }

                    break;
                case TypeCode.UInt32:
                    if (reader.TryGetUInt32(out uint uint32))
                    {
                        return Unsafe.As<uint, TEnum>(ref uint32);
                    }

                    break;
                case TypeCode.UInt64:
                    if (reader.TryGetUInt64(out ulong uint64))
                    {
                        return Unsafe.As<ulong, TEnum>(ref uint64);
                    }

                    break;
                case TypeCode.Int64:
                    if (reader.TryGetInt64(out long int64))
                    {
                        return Unsafe.As<long, TEnum>(ref int64);
                    }

                    break;
                case TypeCode.SByte:
                    if (reader.TryGetSByte(out sbyte byte8))
                    {
                        return Unsafe.As<sbyte, TEnum>(ref byte8);
                    }

                    break;
                case TypeCode.Byte:
                    if (reader.TryGetByte(out byte uByte8))
                    {
                        return Unsafe.As<byte, TEnum>(ref uByte8);
                    }

                    break;
                case TypeCode.Int16:
                    if (reader.TryGetInt16(out short int16))
                    {
                        return Unsafe.As<short, TEnum>(ref int16);
                    }

                    break;
                case TypeCode.UInt16:
                    if (reader.TryGetUInt16(out ushort uint16))
                    {
                        return Unsafe.As<ushort, TEnum>(ref uint16);
                    }

                    break;
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            switch (_enumTypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue(Unsafe.As<TEnum, int>(ref value));
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue(Unsafe.As<TEnum, uint>(ref value));
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(Unsafe.As<TEnum, ulong>(ref value));
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue(Unsafe.As<TEnum, long>(ref value));
                    break;
                case TypeCode.Int16:
                    writer.WriteNumberValue(Unsafe.As<TEnum, short>(ref value));
                    break;
                case TypeCode.UInt16:
                    writer.WriteNumberValue(Unsafe.As<TEnum, ushort>(ref value));
                    break;
                case TypeCode.Byte:
                    writer.WriteNumberValue(Unsafe.As<TEnum, byte>(ref value));
                    break;
                case TypeCode.SByte:
                    writer.WriteNumberValue(Unsafe.As<TEnum, sbyte>(ref value));
                    break;
                default:
                    throw new JsonException();
            }
        }
    }
}
