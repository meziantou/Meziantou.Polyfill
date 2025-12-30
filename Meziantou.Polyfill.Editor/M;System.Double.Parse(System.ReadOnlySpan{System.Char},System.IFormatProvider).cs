using System;
using System.Globalization;

static partial class PolyfillExtensions_Double
{
    extension(double)
    {
        public static double Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => double.Parse(s.ToString(), NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }
}