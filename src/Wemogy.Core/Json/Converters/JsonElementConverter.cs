using System;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Wemogy.Core.Json.Converters
{
    /// <summary>
    /// Newtonsoft.Json does not support System.Text.Json JsonElement type.
    /// This converter allows to use JsonElement with Newtonsoft.Json.
    /// </summary>
    public class JsonElementConverter : JsonConverter<JsonElement>
    {
        public override void WriteJson(JsonWriter writer, JsonElement value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }

        public override JsonElement ReadJson(
            JsonReader reader,
            Type objectType,
            JsonElement existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            // Deserialize the JSON using System.Text.Json and convert it to JsonElement
            var json = JObject.Load(reader).ToString();
            return JsonDocument.Parse(json).RootElement;
        }
    }
}
