using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA512
{
    extension(HMACSHA512)
    {
        public static byte[] HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source)
        {
            using var algorithm = new HMACSHA512(key.ToArray());
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
