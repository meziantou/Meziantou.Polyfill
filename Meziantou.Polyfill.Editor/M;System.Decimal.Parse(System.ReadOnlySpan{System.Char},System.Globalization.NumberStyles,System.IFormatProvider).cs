using System;
using System.Globalization;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static decimal Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = null) => decimal.Parse(s.ToString(), style, provider);
    }
}