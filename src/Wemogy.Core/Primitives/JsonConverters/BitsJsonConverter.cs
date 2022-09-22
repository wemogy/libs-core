using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wemogy.Core.Primitives.JsonConverters
{
    public class BitsJsonConverter : JsonConverter<Bits>
    {
        public override Bits Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Bits(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Bits? value, JsonSerializerOptions options)
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
