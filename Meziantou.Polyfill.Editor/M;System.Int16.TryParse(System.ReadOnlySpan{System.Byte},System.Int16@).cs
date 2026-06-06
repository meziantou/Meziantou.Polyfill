using System;
using System.Globalization;
using System.Text;
static partial class PolyfillExtensions_Int16
{
    extension(short)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out short result) => short.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
