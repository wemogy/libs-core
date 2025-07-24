using System.Security.Cryptography;

namespace Wemogy.Core.Extensions
{
    public static class RsaExtensions
    {
        public static RSA ExportPublicKey(this RSA rsa)
        {
            var publicKeyParameters = rsa.ExportParameters(false);
            var rsaPublic = RSA.Create();
            rsaPublic.ImportParameters(publicKeyParameters);
            return rsaPublic;
        }
    }
}
