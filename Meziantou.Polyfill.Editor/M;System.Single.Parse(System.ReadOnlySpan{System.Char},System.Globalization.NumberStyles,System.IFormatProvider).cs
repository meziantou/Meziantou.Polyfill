using System;
using System.Globalization;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static float Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands, IFormatProvider? provider = null) => float.Parse(s.ToString(), style, provider);
    }
}