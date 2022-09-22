using System;
using System.Collections.Generic;
using Wemogy.Core.Jwt;
using Wemogy.Core.Jwt.Models;
using Wemogy.Core.Tests.Jwt.Models;
using Xunit;

namespace Wemogy.Core.Tests.Jwt;

public class JwtEncoderTests
{
    [Fact]
    public void Encode_ShouldWork()
    {
        // Arrange
        var jwtDescriptor = new JwtDescriptor()
        {
            Subject = "wemogy app",
            Audience = "https://wemogy.cloud",
            Issuer = "https://identity.wemogy.cloud"
        };

        // Act
        var jwt = JwtEncoder.Encode(jwtDescriptor);

        // Assert
        Assert.NotNull(jwt);
    }

    [Fact]
    public void Encode_Object_ShouldWork()
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
            AdditionalClaims = new Dictionary<string, object>()
            {
                {
                    "ext", new ExtendedJwtPayload()
                    {
                        SpaceBlocksTenantId = spaceBlocksTenantId,
                        SpaceBlocksProjectId = spaceBlocksProjectId
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
    }
}
