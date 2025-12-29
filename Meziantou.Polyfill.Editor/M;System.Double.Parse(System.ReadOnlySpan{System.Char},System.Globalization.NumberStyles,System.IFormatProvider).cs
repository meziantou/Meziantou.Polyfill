using System;
using System.Globalization;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static double Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands, IFormatProvider? provider = null) => double.Parse(s.ToString(), style, provider);
    }
}