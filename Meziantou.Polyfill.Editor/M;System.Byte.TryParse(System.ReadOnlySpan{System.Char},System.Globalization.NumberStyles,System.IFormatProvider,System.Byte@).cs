using System;
using System.Globalization;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out byte result) => byte.TryParse(s.ToString(), style, provider, out result);
    }
}