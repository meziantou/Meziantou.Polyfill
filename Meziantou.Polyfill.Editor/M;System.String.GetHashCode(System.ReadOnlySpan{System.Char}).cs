using System;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static int GetHashCode(ReadOnlySpan<char> value)
        {
            return value.ToString().GetHashCode();
        }
    }
}
