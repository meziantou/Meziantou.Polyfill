using System;
using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA384
{
    extension(HMACSHA384)
    {
        public static byte[] HashData(ReadOnlySpan<byte> key, Stream source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            using var algorithm = new HMACSHA384(key.ToArray());
            return algorithm.ComputeHash(source);
        }
    }
}
