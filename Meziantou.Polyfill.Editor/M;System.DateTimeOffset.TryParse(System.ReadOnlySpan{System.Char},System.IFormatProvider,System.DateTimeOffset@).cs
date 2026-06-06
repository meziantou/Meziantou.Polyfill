using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTimeOffset
{
    extension(DateTimeOffset)
    {
        public static bool TryParse(ReadOnlySpan<char> input, IFormatProvider? provider, out DateTimeOffset result) => DateTimeOffset.TryParse(input.ToString(), provider, DateTimeStyles.None, out result);
    }
}
