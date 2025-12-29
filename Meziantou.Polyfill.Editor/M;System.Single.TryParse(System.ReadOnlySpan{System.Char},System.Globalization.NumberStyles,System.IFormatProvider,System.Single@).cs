using System;
using System.Globalization;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out float result) => float.TryParse(s.ToString(), style, provider, out result);
    }
}