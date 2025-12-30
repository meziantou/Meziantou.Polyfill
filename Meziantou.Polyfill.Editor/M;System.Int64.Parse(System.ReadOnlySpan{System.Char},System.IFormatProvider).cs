using System;
using System.Globalization;

static partial class PolyfillExtensions_Int64
{
    extension(long)
    {
        public static long Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => long.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}