using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_SHA512
{
    extension(SHA512)
    {
        public static ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
        {
            if (destination.Length < 64)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = SHA512.Create();
            var hash = algorithm.ComputeHash(source);
            hash.AsMemory().CopyTo(destination);
            return ValueTask.FromResult(hash.Length);
        }
    }
}
