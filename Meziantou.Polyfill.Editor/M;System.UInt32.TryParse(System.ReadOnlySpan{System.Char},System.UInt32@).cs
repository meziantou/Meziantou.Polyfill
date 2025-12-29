using System;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out uint result) => uint.TryParse(s.ToString(), out result);
    }
}