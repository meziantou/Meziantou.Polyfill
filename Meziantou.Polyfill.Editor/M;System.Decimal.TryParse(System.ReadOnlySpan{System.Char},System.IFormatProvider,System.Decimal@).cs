using System;
using System.Globalization;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out decimal result) => decimal.TryParse(s.ToString(), NumberStyles.Number, provider, out result);
    }
}