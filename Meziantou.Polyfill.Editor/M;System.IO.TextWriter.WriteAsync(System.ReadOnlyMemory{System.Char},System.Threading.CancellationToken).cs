using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

static partial class PolyfillExtensions
{
    public static ValueTask WriteAsync(this TextWriter target, ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteAsync(segment.Array!, segment.Offset, segment.Count));
    }
}