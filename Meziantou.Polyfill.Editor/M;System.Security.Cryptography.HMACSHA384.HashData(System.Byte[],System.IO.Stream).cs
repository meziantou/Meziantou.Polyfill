using System;
using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA384
{
    extension(HMACSHA384)
    {
        public static byte[] HashData(byte[] key, Stream source)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            using var algorithm = new HMACSHA384(key);
            return algorithm.ComputeHash(source);
        }
    }
}
