using System;
using System.Globalization;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static sbyte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => sbyte.Parse(s.ToString(), style, provider);
    }
}