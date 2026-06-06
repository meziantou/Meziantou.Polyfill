using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UInt32
{
    extension(uint)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out uint result) => uint.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
