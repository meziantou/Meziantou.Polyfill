using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_SByte
{
    extension(sbyte)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out sbyte result) => sbyte.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Integer, CultureInfo.CurrentCulture, out result);
    }
}
