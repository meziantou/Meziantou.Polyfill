using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTimeOffset
{
    extension(DateTimeOffset)
    {
        public static bool TryParse(string? input, IFormatProvider? provider, out DateTimeOffset result) => DateTimeOffset.TryParse(input, provider, DateTimeStyles.None, out result);
    }
}
