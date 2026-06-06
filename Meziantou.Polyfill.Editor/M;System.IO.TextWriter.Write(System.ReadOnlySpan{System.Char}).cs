using System;
using System.IO;
static partial class PolyfillExtensions
{
    public static void Write(this TextWriter target, ReadOnlySpan<char> buffer) => target.Write(buffer.ToArray());
}
