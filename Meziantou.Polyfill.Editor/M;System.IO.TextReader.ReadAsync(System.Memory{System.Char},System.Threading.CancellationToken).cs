using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

static partial class PolyfillExtensions
{
    public static ValueTask<int> ReadAsync(this TextReader target, Memory<char> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.ReadAsync(segment.Array!, segment.Offset, segment.Count));
    }
}