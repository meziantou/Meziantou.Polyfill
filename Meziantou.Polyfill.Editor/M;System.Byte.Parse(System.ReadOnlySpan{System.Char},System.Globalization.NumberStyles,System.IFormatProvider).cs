using System;
using System.Globalization;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static byte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => byte.Parse(s.ToString(), style, provider);
    }
}