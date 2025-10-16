using System;
using System.Globalization;

static partial class PolyfillExtensions
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out int result) => int.TryParse(s.ToString(), NumberStyles.Integer, provider, out result);
    }
}