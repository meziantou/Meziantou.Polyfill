using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA1
{
    extension(SHA1)
    {
        public static int HashData(ReadOnlySpan<byte> source, Span<byte> destination)
        {
            if (destination.Length < 20)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = SHA1.Create();
            var hash = algorithm.ComputeHash(source.ToArray());
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
