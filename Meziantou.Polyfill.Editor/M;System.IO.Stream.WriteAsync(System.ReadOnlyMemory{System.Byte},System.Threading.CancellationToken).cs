using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

static partial class PolyfillExtensions
{
    public static async ValueTask WriteAsync(this Stream target, ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        await target.WriteAsync(buffer.ToArray(), 0, buffer.Length, cancellationToken);
    }
}