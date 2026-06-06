using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_SHA384
{
    extension(SHA384)
    {
        public static ValueTask<byte[]> HashDataAsync(Stream source, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = SHA384.Create();
            return ValueTask.FromResult(algorithm.ComputeHash(source));
        }
    }
}
