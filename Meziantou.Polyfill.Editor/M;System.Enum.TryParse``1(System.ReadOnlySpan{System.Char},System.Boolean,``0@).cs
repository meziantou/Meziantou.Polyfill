using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse<TEnum>(ReadOnlySpan<char> value, bool ignoreCase, out TEnum result) where TEnum : struct, Enum
        {
            return Enum.TryParse(value.ToString(), ignoreCase, out result);
        }
    }
}
