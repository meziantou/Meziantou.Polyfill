using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static ValueTask<int> HashDataAsync(ReadOnlyMemory<byte> key, Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination.Length < 32)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = new HMACSHA256(key.ToArray());
            var hash = algorithm.ComputeHash(source);
            hash.AsMemory().CopyTo(destination);
            return new ValueTask<int>(hash.Length);
        }
    }
}
