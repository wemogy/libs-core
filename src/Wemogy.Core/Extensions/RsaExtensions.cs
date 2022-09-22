using System.Security.Cryptography;

namespace Wemogy.Core.Extensions
{
    public static class RsaExtensions
    {
        public static RSA ExportPublicKey(this RSA rsa)
        {
            var publicKeyParameters = rsa.ExportParameters(false);
            return RSA.Create(publicKeyParameters);
        }
    }
}
