using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Wemogy.Core.Extensions;

namespace Wemogy.Core.Json.Converters
{
    public class DateTimeUnixTimeSecondsJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (!reader.TryGetInt64(out var timestamp))
            {
                timestamp = long.Parse(reader.GetString() ?? "0");
            }

            return timestamp.FromUnixTimeSeconds();
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options)
        {
            DateTime utcDateTime = dateTimeValue.ToUniversalTime();

            var ticks = utcDateTime.ToUnixTimeSeconds();

            writer.WriteNumberValue(ticks);
        }
    }
}
