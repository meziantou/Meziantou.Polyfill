using System;
using System.Globalization;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static float Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => float.Parse(s.ToString(), NumberStyles.Float | NumberStyles.AllowThousands, provider);
    }
}