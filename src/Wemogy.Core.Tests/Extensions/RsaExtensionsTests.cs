using System.Security.Cryptography;
using Wemogy.Core.Extensions;
using Xunit;

namespace Wemogy.Core.Tests.Extensions;

public class RsaExtensionsTests
{
    [Fact]
    public void ExportPublicKey_ShouldWork()
    {
        // Arrange
        var rsa = RSA.Create();

        // Act
        var publicKey = rsa.ExportPublicKey();
        var rsaExportException = Record.Exception(() => rsa.ExportParameters(true));

        // Assert
        Assert.Null(rsaExportException);
        Assert.Throws<CryptographicException>(() => publicKey.ExportParameters(true));
    }
}
