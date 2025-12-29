using System;
using System.Globalization;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static long Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => long.Parse(s.ToString(), style, provider);
    }
}