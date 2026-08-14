using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<int> ReadBlockAsync(this TextReader target, Memory<char> buffer, CancellationToken cancellationToken = default)
    {
        if (MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            return ReadBlockAsyncCore(target, segment.Array!, segment.Offset, segment.Count, cancellationToken);
        }

        return ReadBlockAsyncFallback(target, buffer, cancellationToken);
    }

    private static async ValueTask<int> ReadBlockAsyncFallback(TextReader target, Memory<char> buffer, CancellationToken cancellationToken)
    {
        var tempArray = new char[buffer.Length];
        var charsRead = await ReadBlockAsyncCore(target, tempArray, 0, tempArray.Length, cancellationToken).ConfigureAwait(false);
        tempArray.AsMemory(0, charsRead).CopyTo(buffer);
        return charsRead;
    }

    private static async ValueTask<int> ReadBlockAsyncCore(TextReader target, char[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        var totalCharsRead = 0;
        int charsRead;
        do
        {
            cancellationToken.ThrowIfCancellationRequested();

            charsRead = await target.ReadAsync(buffer, index + totalCharsRead, count - totalCharsRead).ConfigureAwait(false);
            totalCharsRead += charsRead;
        } while (charsRead > 0 && totalCharsRead < count);

        return totalCharsRead;
    }
}
