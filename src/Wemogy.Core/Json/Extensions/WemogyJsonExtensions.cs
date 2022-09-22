using System.Text.Json;

namespace Wemogy.Core.Json.Extensions
{
    public static class WemogyJsonExtensions
    {
        public static void ApplyWemogyJsonOptions(this JsonSerializerOptions options)
        {
            foreach (var converter in WemogyJson.Converters)
            {
                options.Converters.Add(converter);
            }

            options.PropertyNamingPolicy = WemogyJson.Options.PropertyNamingPolicy;
            options.DefaultIgnoreCondition = WemogyJson.Options.DefaultIgnoreCondition;
            options.ReferenceHandler = WemogyJson.Options.ReferenceHandler;
        }
    }
}
