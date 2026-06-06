using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_SHA384
{
    extension(SHA384)
    {
        public static ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
        {
            if (destination.Length < 48)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = SHA384.Create(); var hash = algorithm.ComputeHash(source);
            hash.AsMemory().CopyTo(destination);
            return ValueTask.FromResult(hash.Length);
        }
    }
}
