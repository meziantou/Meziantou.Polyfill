using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static TEnum Parse<TEnum>(string value, bool ignoreCase) where TEnum : struct, Enum
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }
    }
}
