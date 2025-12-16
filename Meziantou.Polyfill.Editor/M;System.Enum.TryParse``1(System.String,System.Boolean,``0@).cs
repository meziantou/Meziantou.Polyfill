using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse<TEnum>(string? value, bool ignoreCase, out TEnum result) where TEnum : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out result);
        }
    }
}
