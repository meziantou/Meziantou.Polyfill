using System;
using System.Globalization;

static partial class PolyfillExtensions_UIntPtr
{
    extension(UIntPtr)
    {
        public static bool TryParse(string? text, out UIntPtr result)
        {
            if (ulong.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var value) && (UIntPtr.Size == 8 || value <= uint.MaxValue))
            {
                result = new UIntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
