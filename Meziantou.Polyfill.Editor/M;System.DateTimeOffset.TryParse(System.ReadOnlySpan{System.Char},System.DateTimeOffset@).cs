using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTimeOffset
{
    extension(DateTimeOffset)
    {
        public static bool TryParse(ReadOnlySpan<char> input, out DateTimeOffset result) => DateTimeOffset.TryParse(input.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
    }
}
