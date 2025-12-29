using System;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out ulong result) => ulong.TryParse(s.ToString(), out result);
    }
}