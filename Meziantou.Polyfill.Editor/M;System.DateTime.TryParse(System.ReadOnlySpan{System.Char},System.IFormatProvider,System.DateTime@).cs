using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTime
{
    extension(DateTime)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out DateTime result) => DateTime.TryParse(s.ToString(), provider, DateTimeStyles.None, out result);
    }
}
