using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static TEnum[] GetValues<TEnum>() where TEnum : struct, Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}
