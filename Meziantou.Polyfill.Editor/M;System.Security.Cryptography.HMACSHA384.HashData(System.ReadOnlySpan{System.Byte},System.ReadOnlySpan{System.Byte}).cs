using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA384
{
    extension(HMACSHA384)
    {
        public static byte[] HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
        {
            using var algorithm = new HMACSHA384(key.ToArray());
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
