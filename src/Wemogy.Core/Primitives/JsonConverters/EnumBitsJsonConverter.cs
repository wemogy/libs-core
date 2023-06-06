using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wemogy.Core.Primitives.JsonConverters
{
    public class EnumBitsJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(EnumBits<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(EnumBitsJsonConverterInner<>).MakeGenericType(typeToConvert.GetGenericArguments()[0]);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        private class EnumBitsJsonConverterInner<T> : JsonConverter<EnumBits<T>>
            where T : Enum
        {
            public override EnumBits<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new EnumBits<T>(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, EnumBits<T>? value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                    return;
                }

                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
