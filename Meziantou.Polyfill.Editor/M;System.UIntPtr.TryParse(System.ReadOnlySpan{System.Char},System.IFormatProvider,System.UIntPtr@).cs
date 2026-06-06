using System;
using System.Globalization;

static partial class PolyfillExtensions_UIntPtr
{
    extension(UIntPtr)
    {
        public static bool TryParse(ReadOnlySpan<char> text, IFormatProvider? provider, out UIntPtr result)
        {
            if (ulong.TryParse(text.ToString(), NumberStyles.Integer, provider, out var value) && (UIntPtr.Size == 8 || value <= uint.MaxValue))
            {
                result = new UIntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
