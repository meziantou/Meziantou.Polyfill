using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> ReadAsync(this Stream target, Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> segment))
        {
            return new ValueTask<int>(target.ReadAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
        }

        return ReadAsyncFallback(target, buffer, cancellationToken);
    }

    private static async ValueTask<int> ReadAsyncFallback(Stream target, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var tempArray = new byte[buffer.Length];
        var bytesRead = await target.ReadAsync(tempArray, 0, tempArray.Length, cancellationToken).ConfigureAwait(false);
        tempArray.AsSpan(0, bytesRead).CopyTo(buffer.Span);
        return bytesRead;
    }
}
