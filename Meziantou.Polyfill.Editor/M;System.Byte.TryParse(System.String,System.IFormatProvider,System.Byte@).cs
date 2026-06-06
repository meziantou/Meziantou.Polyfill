using System;
using System.Globalization;

static partial class PolyfillExtensions_Byte
{
    extension(byte)
    {
        public static bool TryParse(string? s, IFormatProvider? provider, out byte result) => byte.TryParse(s, NumberStyles.Integer, provider, out result);
    }
}
