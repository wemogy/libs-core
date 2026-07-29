using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using Wemogy.Core.Extensions;

namespace Wemogy.Core.Jwt.Models
{
    public class JwtDescriptor
    {
        public string? Subject { get; set; }
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public List<string> Scopes { get; set; }
        public Dictionary<string, object> AdditionalClaims { get; set; }
        public RSA? PrivateKey { get; set; }

        public JwtDescriptor()
        {
            Scopes = new List<string>();
            AdditionalClaims = new Dictionary<string, object>();
        }

        public JsonObject ToJsonObject()
        {
            var json = new JsonObject();

            if (Subject != null)
            {
                json.Add("sub", Subject);
            }

            if (Audience != null)
            {
                json.Add("aud", Audience);
            }

            if (Issuer != null)
            {
                json.Add("iss", Issuer);
            }

            if (ExpiresAt != null)
            {
                json.Add("exp", ExpiresAt.Value.ToUnixTimeSeconds());
            }

            if (Scopes.Count > 0)
            {
                json.Add("scp", Scopes.Join(" "));
            }

            foreach (var claim in AdditionalClaims)
            {
                var obj = claim.Value.ToJson().FromJson<JsonObject>();
                json.Add(claim.Key, obj);
            }

            return json;
        }
    }
}
