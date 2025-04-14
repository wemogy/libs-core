using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Wemogy.Core.Json.ConverterFactories;
using Wemogy.Core.Json.Converters;
using TypeConverter = Wemogy.Core.Json.Converters.TypeConverter;

namespace Wemogy.Core.Json
{
    public static class WemogyJson
    {
        public static IList<JsonConverter> Converters => new List<JsonConverter>()
        {
            new DateTimeUnixEpochJsonConverter(),
            new EnumJsonConverterFactory(),
            new TypeConverter(),
            new DoubleTrimConverter()
        };

        public static JsonSerializerOptions Options
        {
            get
            {
                var options = new JsonSerializerOptions();

                foreach (var converter in Converters)
                {
                    options.Converters.Add(converter);
                }

                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                return options;
            }
        }
    }
}
