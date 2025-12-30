using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out uint result) => uint.TryParse(s.ToString(), style, provider, out result);
    }
}