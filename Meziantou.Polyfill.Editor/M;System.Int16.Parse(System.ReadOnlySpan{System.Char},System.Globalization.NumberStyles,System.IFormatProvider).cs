using System;
using System.Globalization;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static short Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => short.Parse(s.ToString(), style, provider);
    }
}