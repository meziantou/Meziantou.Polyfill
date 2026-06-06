using System;
using System.Globalization;
using System.Text;
static partial class PolyfillExtensions_UInt16
{
    extension(ushort)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out ushort result) => ushort.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
