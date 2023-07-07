using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static int ReadAtLeast(this Stream target, Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true)
    {
        if (minimumBytes < 0)
             throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Non-negative number required");

        if (buffer.Length < minimumBytes)
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Must not be greater than the length of the buffer.");

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = target.Read(buffer.Slice(totalRead));
            if (read == 0)
            {
                if (throwOnEndOfStream)
                    throw new EndOfStreamException("Unable to read beyond the end of the stream.");

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }
}
