using System;
using System.Globalization;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static short Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => short.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}