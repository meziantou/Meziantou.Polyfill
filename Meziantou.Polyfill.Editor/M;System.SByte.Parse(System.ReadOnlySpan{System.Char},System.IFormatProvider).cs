using System;
using System.Globalization;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static sbyte Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => sbyte.Parse(s.ToString(), NumberStyles.Integer, provider);
    }
}