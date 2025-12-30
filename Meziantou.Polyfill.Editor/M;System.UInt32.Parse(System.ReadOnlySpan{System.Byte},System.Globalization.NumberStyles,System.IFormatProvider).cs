using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static uint Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = null) => uint.Parse(Encoding.UTF8.GetString(utf8Text), style, provider);
    }
}