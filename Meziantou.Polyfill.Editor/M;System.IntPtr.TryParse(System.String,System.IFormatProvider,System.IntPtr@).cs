using System;
using System.Globalization;

static partial class PolyfillExtensions_IntPtr
{
    extension(IntPtr)
    {
        public static bool TryParse(string? text, IFormatProvider? provider, out IntPtr result)
        {
            if (long.TryParse(text, NumberStyles.Integer, provider, out var value) && (IntPtr.Size == 8 || (value >= int.MinValue && value <= int.MaxValue)))
            {
                result = new IntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
