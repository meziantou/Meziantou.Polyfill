using System;
using System.Globalization;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static decimal Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => decimal.Parse(s.ToString(), NumberStyles.Number, provider);
    }
}