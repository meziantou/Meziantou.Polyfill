using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA384
{
    extension(HMACSHA384)
    {
        public static int HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (destination.Length < 48)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = new HMACSHA384(key.ToArray());
            var hash = algorithm.ComputeHash(source.ToArray());
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
