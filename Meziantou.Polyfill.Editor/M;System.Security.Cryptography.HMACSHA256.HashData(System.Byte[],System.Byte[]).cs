using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static byte[] HashData(byte[] key, byte[] source)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            using var algorithm = new HMACSHA256(key);
            return algorithm.ComputeHash(source);
        }
    }
}
