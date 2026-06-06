using System;
using System.IO;
static partial class PolyfillExtensions
{
    public static void WriteLine(this TextWriter target, ReadOnlySpan<char> buffer) => target.WriteLine(buffer.ToArray());
}
