using System;
using System.Globalization;

static partial class PolyfillExtensions_DateTime
{
    extension(DateTime)
    {
        public static bool TryParse(string? s, IFormatProvider? provider, out DateTime result) => DateTime.TryParse(s, provider, DateTimeStyles.None, out result);
    }
}
