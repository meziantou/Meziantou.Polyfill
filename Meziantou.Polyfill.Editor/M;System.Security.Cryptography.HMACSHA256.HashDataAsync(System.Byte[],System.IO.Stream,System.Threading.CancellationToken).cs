using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_HMACSHA256
{
    extension(HMACSHA256)
    {
        public static ValueTask<byte[]> HashDataAsync(byte[] key, Stream source, CancellationToken cancellationToken = default)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = new HMACSHA256(key);
            return new ValueTask<byte[]>(algorithm.ComputeHash(source));
        }
    }
}
