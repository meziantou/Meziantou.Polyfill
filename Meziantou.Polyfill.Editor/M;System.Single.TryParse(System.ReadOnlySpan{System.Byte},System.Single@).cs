using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_Single
{
    extension(float)
    {
        public static bool TryParse(ReadOnlySpan<byte> value, out float result) => float.TryParse(Encoding.UTF8.GetString(value), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out result);
    }
}
