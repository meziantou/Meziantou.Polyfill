using System;
using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_HMACSHA512
{
    extension(HMACSHA512)
    {
        public static int HashData(ReadOnlySpan<byte> key, Stream source, Span<byte> destination)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination.Length < 64)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = new HMACSHA512(key.ToArray());
            var hash = algorithm.ComputeHash(source);
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
