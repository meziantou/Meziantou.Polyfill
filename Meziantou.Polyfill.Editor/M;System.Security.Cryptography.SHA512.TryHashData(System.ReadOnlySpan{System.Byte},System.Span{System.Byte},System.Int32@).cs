using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA512
{
    extension(SHA512)
    {
        public static bool TryHashData(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
        {
            if (destination.Length < 64)
            {
                bytesWritten = 0;
                return false;
            }

            using var algorithm = SHA512.Create();
            var hash = algorithm.ComputeHash(source.ToArray());
            hash.AsSpan().CopyTo(destination);
            bytesWritten = hash.Length;
            return true;
        }
    }
}
