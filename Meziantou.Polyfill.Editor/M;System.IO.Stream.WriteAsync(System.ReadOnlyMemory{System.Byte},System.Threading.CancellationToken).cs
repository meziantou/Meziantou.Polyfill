using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

static partial class PolyfillExtensions
{
    public static ValueTask WriteAsync(this Stream target, ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (MemoryMarshal.TryGetArray(buffer, out ArraySegment<byte> segment))
        {
            return new ValueTask(target.WriteAsync(segment.Array!, segment.Offset, segment.Count, cancellationToken));
        }

        return new ValueTask(target.WriteAsync(buffer.ToArray(), 0, buffer.Length, cancellationToken));
    }
}