using System;
using System.Globalization;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static byte Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => byte.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}