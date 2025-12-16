using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse<TEnum>(ReadOnlySpan<char> value, out TEnum result) where TEnum : struct, Enum
        {
            return Enum.TryParse(value.ToString(), out result);
        }
    }
}
