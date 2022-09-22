using System.Security.Cryptography;
using System.Text.Json;
using JWT.Algorithms;
using JWT.Builder;

namespace Wemogy.Core.Jwt
{
    public static class JwtDecoder
    {
        /// <returns>The payload of the JWT Token</returns>
        public static string Decode(string jwtToken)
        {
            var json = JwtBuilder.Create()
                .DoNotVerifySignature()
                .Decode(jwtToken);

            return json;
        }

        /// <summary>
        ///     Decodes the JWT token payload using the following conventions:
        ///     - snake-case property name
        ///     - timestamps/dates as unix time in SECONDS
        /// </summary>
        public static TPayload Decode<TPayload>(string jwtToken)
            where TPayload : class
        {
            var json = Decode(jwtToken);

            return JsonSerializer.Deserialize<TPayload>(
                json,
                JwtDefaults.JsonSerializerOptions) !;
        }

        public static string DecodeAndVerifyJwtToken(string jwtToken, RSA publicKey)
        {
            var json = JwtBuilder.Create()
                .WithAlgorithm(new RS256Algorithm(publicKey))
                .MustVerifySignature()
                .Decode(jwtToken);

            return json;
        }
    }
}
