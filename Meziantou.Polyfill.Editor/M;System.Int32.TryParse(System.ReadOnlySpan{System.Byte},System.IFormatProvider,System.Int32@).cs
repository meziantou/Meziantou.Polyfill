using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out int result) => int.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider, out result);
    }
}