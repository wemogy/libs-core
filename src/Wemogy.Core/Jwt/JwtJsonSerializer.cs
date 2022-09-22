using System;
using System.Text.Json;
using JWT;

namespace Wemogy.Core.Jwt
{
    public class JwtJsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(
                obj,
                JwtDefaults.JsonSerializerOptions);
        }

        public object Deserialize(Type type, string json)
        {
            return JsonSerializer.Deserialize(
                json,
                type,
                JwtDefaults.JsonSerializerOptions) !;
        }
    }
}
