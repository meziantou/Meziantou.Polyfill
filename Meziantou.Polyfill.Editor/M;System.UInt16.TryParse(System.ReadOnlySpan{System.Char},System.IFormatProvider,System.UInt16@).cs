using System;
using System.Globalization;

static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out ushort result) => ushort.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);
    }
}