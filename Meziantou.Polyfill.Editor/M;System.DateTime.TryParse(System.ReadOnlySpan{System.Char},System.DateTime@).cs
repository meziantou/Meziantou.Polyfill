using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTime
{
    extension(DateTime)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out DateTime result) => DateTime.TryParse(s.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
    }
}
