using System;
using System.Globalization;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out double result) => double.TryParse(s.ToString(), style, provider, out result);
    }
}