using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA1
{
    extension(SHA1)
    {
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var algorithm = SHA1.Create();
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
