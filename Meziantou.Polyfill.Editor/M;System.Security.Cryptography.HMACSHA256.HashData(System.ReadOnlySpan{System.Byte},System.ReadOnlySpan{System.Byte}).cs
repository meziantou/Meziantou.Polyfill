using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static byte[] HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
        {
            using var algorithm = new HMACSHA256(key.ToArray());
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
