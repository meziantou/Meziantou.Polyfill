using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static ValueTask<byte[]> HashDataAsync(ReadOnlyMemory<byte> key, Stream source, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = new HMACSHA256(key.ToArray());
            return new ValueTask<byte[]>(algorithm.ComputeHash(source));
        }
    }
}
