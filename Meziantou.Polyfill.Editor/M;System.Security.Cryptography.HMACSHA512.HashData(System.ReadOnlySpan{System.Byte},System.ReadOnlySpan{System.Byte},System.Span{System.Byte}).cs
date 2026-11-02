using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA512
{
    extension(HMACSHA512)
    {
        public static int HashData(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (destination.Length < 64)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = new HMACSHA512(key.ToArray());
            var hash = algorithm.ComputeHash(source.ToArray());
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
