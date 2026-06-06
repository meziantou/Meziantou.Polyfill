using System;
using System.Globalization;

static partial class PolyfillExtensions_UIntPtr
{
    extension(UIntPtr)
    {
        public static bool TryParse(string? text, NumberStyles style, IFormatProvider? provider, out UIntPtr result)
        {
            if (ulong.TryParse(text, style, provider, out var value) && (UIntPtr.Size == 8 || value <= uint.MaxValue))
            {
                result = new UIntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
