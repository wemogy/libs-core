using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Wemogy.Core.Json.Converters;

namespace Wemogy.Core.Json.ConverterFactories
{
    public class EnumJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                GetEnumConverterType(typeToConvert)) !;
        }

        // thanks to: https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/Serialization/Converters/Value/EnumConverterFactory.cs
        [SuppressMessage(
            "ReflectionAnalysis",
            "IL2055:MakeGenericType",
            Justification = "'EnumConverter<T> where T : struct' implies 'T : new()', so the trimmer is warning calling MakeGenericType here because enumType's constructors are not annotated. But EnumConverter doesn't call new T(), so this is safe.")]
        private static Type GetEnumConverterType(Type enumType) =>
            typeof(EnumJsonConverter<>).MakeGenericType(enumType);
    }
}
