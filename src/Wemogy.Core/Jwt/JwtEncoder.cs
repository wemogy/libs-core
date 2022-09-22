using System;
using System.Linq;
using JWT.Algorithms;
using JWT.Builder;
using Wemogy.Core.Extensions;
using Wemogy.Core.Jwt.Models;

namespace Wemogy.Core.Jwt
{
    public static class JwtEncoder
    {
        public static string Encode(JwtDescriptor jwtDescriptor)
        {
            var jwtBuilder = JwtBuilder.Create();

            if (jwtDescriptor.PrivateKey != null)
            {
                jwtBuilder.WithAlgorithm(new RS256Algorithm(jwtDescriptor.PrivateKey, jwtDescriptor.PrivateKey));
            }
            else
            {
                jwtBuilder.WithAlgorithm(new NoneAlgorithm());

                // even when we use the NoneAlgorithm, the JwtBuilder expect that the key is not null
                // therefore we set it to an empty array
                jwtBuilder.WithSecret(Array.Empty<byte>());
            }

            if (jwtDescriptor.ExpiresAt.HasValue)
            {
                jwtBuilder.AddClaim(ClaimName.ExpirationTime, jwtDescriptor.ExpiresAt.Value.ToDateTimeOffset().ToUnixTimeSeconds());
            }

            if (jwtDescriptor.Subject != null)
            {
                jwtBuilder.AddClaim(ClaimName.Subject, jwtDescriptor.Subject);
            }

            if (jwtDescriptor.Audience != null)
            {
                jwtBuilder.AddClaim(ClaimName.Audience, jwtDescriptor.Audience);
            }

            if (jwtDescriptor.Issuer != null)
            {
                jwtBuilder.AddClaim(ClaimName.Issuer, jwtDescriptor.Issuer);
            }

            if (jwtDescriptor.Scopes.Any())
            {
                jwtBuilder.AddClaim(
                    "scp",
                    jwtDescriptor.Scopes);
            }

            foreach (var additionalClaim in jwtDescriptor.AdditionalClaims)
            {
                jwtBuilder.AddClaim(additionalClaim.Key, additionalClaim.Value);
            }

            var token = jwtBuilder
                .WithSerializer(new JwtJsonSerializer())
                .Encode();
            return token;
        }
    }
}
