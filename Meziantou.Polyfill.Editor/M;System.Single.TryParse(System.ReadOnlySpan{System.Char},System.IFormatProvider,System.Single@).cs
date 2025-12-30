using System;
using System.Globalization;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out float result) => float.TryParse(s.ToString(), NumberStyles.Float | NumberStyles.AllowThousands, provider, out result);
    }
}