using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async ValueTask<int> ReadAsync(this Stream target, Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var read = await target.ReadAsync(segment.Array!, 0, buffer.Length);
        return read;
    }
}
