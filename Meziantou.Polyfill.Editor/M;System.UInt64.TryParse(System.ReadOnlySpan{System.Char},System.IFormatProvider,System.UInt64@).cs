using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt64
{
    extension(ulong)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out ulong result) => ulong.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);
    }
}