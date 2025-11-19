using System;

static partial class PolyfillExtensions
{
    extension(Convert)
    {
        public static int ToInt32(ReadOnlySpan<char> value)
        {
            return int.Parse(value);
        }
    }
}
