using System;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static bool TryParse(ReadOnlySpan<char> s, out decimal result) => decimal.TryParse(s.ToString(), out result);
    }
}