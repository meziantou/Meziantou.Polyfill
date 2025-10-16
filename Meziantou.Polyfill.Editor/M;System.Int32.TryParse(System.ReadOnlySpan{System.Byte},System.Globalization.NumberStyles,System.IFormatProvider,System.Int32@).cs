using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions
{
    extension(int)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out int result) => int.TryParse(Encoding.UTF8.GetString(utf8Text), style, provider, out result);
    }
}