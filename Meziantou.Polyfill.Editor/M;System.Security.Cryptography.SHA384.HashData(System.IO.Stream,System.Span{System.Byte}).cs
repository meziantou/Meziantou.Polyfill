using System;
using System.IO;
using System.Security.Cryptography;
static partial class PolyfillExtensions_SHA384
{
    extension(SHA384)
    {
        public static int HashData(Stream source, Span<byte> destination)
        {
            if (destination.Length < 48)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            using var algorithm = SHA384.Create();
            var hash = algorithm.ComputeHash(source);
            hash.AsSpan().CopyTo(destination);
            return hash.Length;
        }
    }
}
