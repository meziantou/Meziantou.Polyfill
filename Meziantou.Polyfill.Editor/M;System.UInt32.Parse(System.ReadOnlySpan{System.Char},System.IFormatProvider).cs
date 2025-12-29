using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static uint Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => uint.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}