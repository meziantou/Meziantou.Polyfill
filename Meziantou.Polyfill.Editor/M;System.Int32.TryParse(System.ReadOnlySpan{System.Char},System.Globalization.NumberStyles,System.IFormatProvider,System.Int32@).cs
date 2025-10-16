using System;
using System.Globalization;

static partial class PolyfillExtensions
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out int result) => int.TryParse(s.ToString(), style, provider, out result);
    }
}