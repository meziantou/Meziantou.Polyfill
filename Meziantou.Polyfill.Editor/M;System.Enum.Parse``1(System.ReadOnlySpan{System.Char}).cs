using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static TEnum Parse<TEnum>(ReadOnlySpan<char> value) where TEnum : struct, Enum
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
        }
    }
}
