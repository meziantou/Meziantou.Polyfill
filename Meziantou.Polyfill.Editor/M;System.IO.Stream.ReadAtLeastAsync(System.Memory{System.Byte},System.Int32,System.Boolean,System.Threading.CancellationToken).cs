using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async ValueTask<int> ReadAtLeastAsync(this Stream target, Memory<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true, CancellationToken cancellationToken = default)
    {
        if (minimumBytes < 0)
             throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Non-negative number required");

        if (buffer.Length < minimumBytes)
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Must not be greater than the length of the buffer.");

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = await target.ReadAsync(buffer.Slice(totalRead), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                if (throwOnEndOfStream)
                {
                    throw new EndOfStreamException("Unable to read beyond the end of the stream.");
                }

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }
}
