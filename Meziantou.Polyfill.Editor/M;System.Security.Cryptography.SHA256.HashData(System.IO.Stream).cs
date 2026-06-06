using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA256
{
    extension(SHA256)
    {
        public static byte[] HashData(Stream source)
        {
            using var algorithm = SHA256.Create();
            return algorithm.ComputeHash(source);
        }
    }
}
