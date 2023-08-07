using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static void Write(this Stream target, ReadOnlySpan<byte> buffer)
    {
        target.Write(buffer.ToArray(), 0, buffer.Length);
    }
}