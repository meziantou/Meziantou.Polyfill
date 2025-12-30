using System;
using System.Globalization;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static int Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => int.Parse(s.ToString(), style, provider);
    }
}