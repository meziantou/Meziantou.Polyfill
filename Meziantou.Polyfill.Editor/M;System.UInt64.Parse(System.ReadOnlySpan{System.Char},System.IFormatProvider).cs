using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static ulong Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => ulong.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}