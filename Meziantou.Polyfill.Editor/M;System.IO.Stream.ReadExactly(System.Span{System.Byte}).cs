using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static void ReadExactly(this Stream target, Span<byte> buffer)
    {
        int totalRead = 0;
        while (totalRead < buffer.Length)
        {
            int read = target.Read(buffer.Slice(totalRead));
            if (read == 0)
                throw new EndOfStreamException("Unable to read beyond the end of the stream.");

            totalRead += read;
        }
    }
}