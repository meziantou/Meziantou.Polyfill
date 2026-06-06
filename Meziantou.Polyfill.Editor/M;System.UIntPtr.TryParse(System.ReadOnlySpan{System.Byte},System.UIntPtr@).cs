using System;
using System.Globalization;
using System.Text;

static partial class PolyfillExtensions_UIntPtr
{
    extension(UIntPtr)
    {
        public static bool TryParse(ReadOnlySpan<byte> text, out UIntPtr result)
        {
            if (ulong.TryParse(Encoding.UTF8.GetString(text), NumberStyles.Integer, CultureInfo.CurrentCulture, out var value) && (UIntPtr.Size == 8 || value <= uint.MaxValue))
            {
                result = new UIntPtr(value);
                return true;
            }

            result = default;
            return false;
        }
    }
}
