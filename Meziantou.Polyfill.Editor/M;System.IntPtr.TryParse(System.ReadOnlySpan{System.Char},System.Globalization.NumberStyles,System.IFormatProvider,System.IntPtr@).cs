using System;
using System.Globalization;

static partial class PolyfillExtensions_IntPtr
{
    extension(IntPtr)
    {
        public static bool TryParse(ReadOnlySpan<char> text, NumberStyles style, IFormatProvider? provider, out IntPtr result)
        {
            if (long.TryParse(text.ToString(), style, provider, out var value) && (IntPtr.Size == 8 || (value >= int.MinValue && value <= int.MaxValue)))
            {
                result = new IntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
