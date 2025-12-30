using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out ushort result) => ushort.TryParse(s.ToString(), style, provider, out result);
    }
}