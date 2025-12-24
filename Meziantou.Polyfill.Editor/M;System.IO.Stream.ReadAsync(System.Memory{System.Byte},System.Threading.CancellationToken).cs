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

        return new ValueTask<int>(target.ReadAsync(buffer.ToArray(), 0, buffer.Length, cancellationToken));
    }
}
