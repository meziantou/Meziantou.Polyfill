using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static ulong Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => ulong.Parse(s.ToString(), style, provider);
    }
}