using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_SHA1
{
    extension(SHA1)
    {
        public static async ValueTask<int> HashDataAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
        {
            if (destination.Length < 20)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            cancellationToken.ThrowIfCancellationRequested();

            var hash = await Task.Run(() =>
            {
                using var algorithm = SHA1.Create();
                return algorithm.ComputeHash(source);
            }, cancellationToken).ConfigureAwait(false);

            hash.AsMemory().CopyTo(destination);
            return hash.Length;
        }
    }
}
