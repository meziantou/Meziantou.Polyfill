using System;
using System.Security.Cryptography;

static partial class PolyfillExtensions_MD5
{
    extension(MD5)
    {
        public static byte[] HashData(ReadOnlySpan<byte> source)
        {
            using var md5 = MD5.Create();
            return md5.ComputeHash(source.ToArray());
        }

    }
}
