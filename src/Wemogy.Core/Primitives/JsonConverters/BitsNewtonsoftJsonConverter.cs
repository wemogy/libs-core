using System;
using Newtonsoft.Json;

namespace Wemogy.Core.Primitives.JsonConverters
{
    public class BitsNewtonsoftJsonConverter : JsonConverter<Bits>
    {
        public override void WriteJson(JsonWriter writer, Bits? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.ToString());
        }

        public override Bits ReadJson(
            JsonReader reader,
            Type objectType,
            Bits? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value is string stringValue)
            {
                return new Bits(stringValue);
            }

            return new Bits();
        }
    }
}
