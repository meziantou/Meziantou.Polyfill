using System;
using System.Globalization;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static bool TryParse(string? s, IFormatProvider? provider, out decimal result) => decimal.TryParse(s, NumberStyles.Number, provider, out result);
    }
}
