using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA384
{
    extension(SHA384)
    {
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var algorithm = SHA384.Create();
            return algorithm.ComputeHash(source.ToArray());
        }
    }
}
