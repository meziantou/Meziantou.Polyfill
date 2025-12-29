using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static decimal Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = null) => decimal.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}