using System;
using System.Collections.Generic;
using System.Security.Cryptography;

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
    }
}
