using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTimeOffset
{
    extension(DateTimeOffset)
    {
        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result) => DateTimeOffset.TryParseExact(input.ToString(), format.ToString(), provider, styles, out result);
    }
}
