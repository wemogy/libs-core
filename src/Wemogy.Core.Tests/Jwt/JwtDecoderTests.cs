using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using JWT.Exceptions;
using Wemogy.Core.Extensions;
using Wemogy.Core.Jwt;
using Wemogy.Core.Jwt.Models;
using Wemogy.Core.Tests.Jwt.Models;
using Xunit;

namespace Wemogy.Core.Tests.Jwt;

public class JwtDecoderTests
{
    [Fact]
    public void Decode_ShouldWork()
    {
        // Arrange
        var jwtDescriptor = new JwtDescriptor()
        {
            Subject = "wemogy app",
            Audience = "https://wemogy.cloud",
            Issuer = "https://identity.wemogy.cloud",
            ExpiresAt = DateTime.UtcNow.AddDays(2)
        };
        var jwt = JwtEncoder.Encode(jwtDescriptor);

        // Act
        var json = JwtDecoder.Decode(jwt);

        // Assert
        Assert.NotNull(json);
    }

    [Fact]
    public void Decode_Generic_ShouldWork()
    {
        // Arrange
        var spaceBlocksTenantId = "tenant-GUID";
        var spaceBlocksProjectId = "project-GUID";
        var jwtDescriptor = new JwtDescriptor()
        {
            Subject = "wemogy app",
            Audience = "https://wemogy.cloud",
            Issuer = "https://identity.wemogy.cloud",
            ExpiresAt = new DateTime(2022, 05, 14, 20, 15, 30, DateTimeKind.Utc),
            Scopes = new List<string>()
            {
                "read:secrets",
                "offline_access",
            },
            AdditionalClaims = new Dictionary<string, object>()
            {
                {
                    "ext", new Dictionary<string, string>()
                    {
                        { "space_blocks_tenant_id", spaceBlocksTenantId },
                        { "space_blocks_project_id", spaceBlocksProjectId }
                    }
                }
            }
        };
        var jwt = JwtEncoder.Encode(jwtDescriptor);

        // Act
        var payload = JwtDecoder.Decode<SimpleJwtPayload>(jwt);

        // Assert
        Assert.NotNull(payload);
        Assert.Equal(jwtDescriptor.Subject, payload.Sub);
        Assert.Equal(jwtDescriptor.Audience, payload.Aud);
        Assert.Equal(jwtDescriptor.ExpiresAt, payload.Exp);
        Assert.Equal(spaceBlocksTenantId, payload.Ext.SpaceBlocksTenantId);
        Assert.Equal(spaceBlocksProjectId, payload.Ext.SpaceBlocksProjectId);
        Assert.Equal(2, payload.Scp.Count);
        Assert.Contains(
            "read:secrets",
            payload.Scp);
    }

    [Fact]
    public void DecodeAndVerifyJwtToken_ShouldWork()
    {
        // Arrange
        var rsa = RSA.Create();
        var publicKey = rsa.ExportPublicKey();
        var invalidPublicKey = RSA.Create().ExportPublicKey();
        var jwtDescriptor = new JwtDescriptor()
        {
            Subject = "wemogy app",
            Audience = "https://wemogy.cloud",
            Issuer = "https://identity.wemogy.cloud",
            ExpiresAt = DateTime.UtcNow.AddDays(2),
            PrivateKey = rsa
        };
        var jwt = JwtEncoder.Encode(jwtDescriptor);

        // Act
        var validPublicKeyException = Record.Exception(() => JwtDecoder.DecodeAndVerifyJwtToken(jwt, publicKey));
        var invalidPublicKeyException =
            Record.Exception(() => JwtDecoder.DecodeAndVerifyJwtToken(jwt, invalidPublicKey));

        // Assert
        Assert.Null(validPublicKeyException);
        Assert.IsType<SignatureVerificationException>(invalidPublicKeyException);
    }
}
