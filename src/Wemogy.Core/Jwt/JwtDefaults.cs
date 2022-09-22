using System.Text.Json;
using Wemogy.Core.Json.Converters;
using Wemogy.Core.Json.NamingPolicies;

namespace Wemogy.Core.Jwt
{
    public class JwtDefaults
    {
        public static JsonSerializerOptions JsonSerializerOptions =>
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                Converters =
                {
                    new DateTimeUnixTimeSecondsJsonConverter()
                }
            };
    }
}
