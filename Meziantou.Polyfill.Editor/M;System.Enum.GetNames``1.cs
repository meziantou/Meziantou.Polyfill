using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static string[] GetNames<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetNames(typeof(TEnum));
        }
    }
}