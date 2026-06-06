using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA512
{
    extension(SHA512)
    {
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var algorithm = SHA512.Create();
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
