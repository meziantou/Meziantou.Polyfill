using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static ushort Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => ushort.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}