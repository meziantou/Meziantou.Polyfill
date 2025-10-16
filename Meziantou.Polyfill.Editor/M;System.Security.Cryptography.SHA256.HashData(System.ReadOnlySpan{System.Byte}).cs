using System;
using System.Security.Cryptography;

static partial class PolyfillExtensions
{
    extension(SHA256)
    {
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(source.ToArray());
        }

    }
}
