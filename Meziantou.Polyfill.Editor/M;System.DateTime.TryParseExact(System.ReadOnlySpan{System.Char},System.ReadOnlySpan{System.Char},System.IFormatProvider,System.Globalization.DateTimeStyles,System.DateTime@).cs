using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTime
{
    extension(DateTime)
    {
        public static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, IFormatProvider? provider, DateTimeStyles style, out DateTime result) => DateTime.TryParseExact(s.ToString(), format.ToString(), provider, style, out result);
    }
}
