using System;
using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA256
{
    extension(SHA256)
    {
        public static int HashData(Stream source, Span<byte> destination)
        {
            if (destination.Length < 32)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = SHA256.Create();
            var hash = algorithm.ComputeHash(source);
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
