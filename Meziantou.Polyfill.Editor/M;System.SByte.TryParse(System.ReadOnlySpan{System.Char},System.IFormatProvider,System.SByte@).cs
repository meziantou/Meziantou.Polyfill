using System;
using System.Globalization;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out sbyte result) => sbyte.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);
    }
}