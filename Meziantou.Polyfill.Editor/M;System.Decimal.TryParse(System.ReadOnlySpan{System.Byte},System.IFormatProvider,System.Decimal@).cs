using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Decimal
{
    extension(decimal)
    {
        public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out decimal result) => decimal.TryParse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Number, provider, out result);
    }
}