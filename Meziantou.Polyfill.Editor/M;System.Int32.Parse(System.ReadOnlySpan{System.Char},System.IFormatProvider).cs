using System;
using System.Globalization;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static int Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => int.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}