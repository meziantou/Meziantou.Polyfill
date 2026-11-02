using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA512
{
    extension(HMACSHA512)
    {
        public static byte[] HashData(byte[] key, byte[] source)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            using var algorithm = new HMACSHA512(key);
            return algorithm.ComputeHash(source);
        }
    }
}
