using System;
using System.Globalization;

static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out short result) => short.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);
    }
}