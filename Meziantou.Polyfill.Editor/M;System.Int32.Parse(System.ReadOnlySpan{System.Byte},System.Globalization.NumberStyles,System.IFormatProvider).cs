using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Int32
{
    extension(int)
    {
        public static int Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => int.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}