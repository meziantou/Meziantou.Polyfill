using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static int Read(this Stream target, Span<byte> buffer)
    {
        var bufferTemp = new byte[buffer.Length];
        var read = target.Read(bufferTemp, 0, bufferTemp.Length);
        bufferTemp.AsSpan(0, read).CopyTo(buffer);
        return read;
    }
}
