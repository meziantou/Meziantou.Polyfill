using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }
    }
}
