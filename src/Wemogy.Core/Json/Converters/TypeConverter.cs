using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Wemogy.Core.Errors;

namespace Wemogy.Core.Json.Converters
{
    /// <summary>
    /// Converts a <see cref="Type" /> to and from JSON.
    /// This is not supported by the default <see cref="JsonSerializer" />.
    /// </summary>
    public class TypeConverter : JsonConverter<Type>
    {
        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? typeName = reader.GetString();

            if (typeName is null)
            {
                throw Error.Unexpected(
                    "TypeConverterReadNull",
                    "TypeConverter read null value. This should not happen.");
            }

            var resolvedType = Type.GetType(typeName);

            if (resolvedType is null)
            {
                throw Error.Unexpected(
                    "TypeNotResolved",
                    $"TypeConverter could not resolve type {typeName}.");
            }

            return resolvedType;
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.AssemblyQualifiedName);
        }
    }
}
