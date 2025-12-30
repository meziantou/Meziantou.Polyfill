using System;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out ushort result) => ushort.TryParse(s.ToString(), out result);
    }
}