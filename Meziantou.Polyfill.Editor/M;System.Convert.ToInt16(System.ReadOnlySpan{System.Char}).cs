using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static short ToInt16(ReadOnlySpan<char> value)
        {
            return short.Parse(value, provider: null);
        }
    }
}
