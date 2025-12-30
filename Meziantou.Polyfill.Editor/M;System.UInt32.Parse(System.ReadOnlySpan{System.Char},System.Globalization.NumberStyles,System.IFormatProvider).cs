using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static uint Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => uint.Parse(s.ToString(), style, provider);
    }
}