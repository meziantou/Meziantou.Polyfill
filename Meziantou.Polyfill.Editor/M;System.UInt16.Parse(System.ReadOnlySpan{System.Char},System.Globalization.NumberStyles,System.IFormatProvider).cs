using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static ushort Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => ushort.Parse(s.ToString(), style, provider);
    }
}