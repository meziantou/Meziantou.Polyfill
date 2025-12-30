using System;
using System.Globalization;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out double result) => double.TryParse(s.ToString(), NumberStyles.Float | NumberStyles.AllowThousands, provider, out result);
    }
}