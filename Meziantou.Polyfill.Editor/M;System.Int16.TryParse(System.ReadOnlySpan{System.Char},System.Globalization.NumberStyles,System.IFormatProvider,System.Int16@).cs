using System;
using System.Globalization;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out short result) => short.TryParse(s.ToString(), style, provider, out result);
    }
}