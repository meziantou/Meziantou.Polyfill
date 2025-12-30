using System;
using System.Globalization;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out long result) => long.TryParse(s.ToString(), style, provider, out result);
    }
}