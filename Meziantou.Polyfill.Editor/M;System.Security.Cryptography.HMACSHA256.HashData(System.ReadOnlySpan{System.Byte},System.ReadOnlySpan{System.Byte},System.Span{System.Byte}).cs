using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static int HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (destination.Length < 32)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = new HMACSHA256(key.ToArray());
            var hash = algorithm.ComputeHash(source.ToArray());
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
