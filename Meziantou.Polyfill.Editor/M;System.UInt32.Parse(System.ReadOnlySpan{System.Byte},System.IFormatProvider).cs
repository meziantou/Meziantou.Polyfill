using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static uint Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => uint.Parse(Encoding.UTF8.GetString(utf8Text), NumberStyles.Integer, provider);
    }
}