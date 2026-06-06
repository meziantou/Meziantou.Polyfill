using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
static partial class PolyfillExtensions_SHA512
{
    extension(SHA512)
    {
        public static ValueTask<byte[]> HashDataAsync(Stream source, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            using var algorithm = SHA512.Create();
            return ValueTask.FromResult(algorithm.ComputeHash(source));
        }
    }
}
