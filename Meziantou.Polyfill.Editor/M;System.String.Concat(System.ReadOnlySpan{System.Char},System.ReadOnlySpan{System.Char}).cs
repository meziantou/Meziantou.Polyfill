using System;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1)
        {
            return str0.ToString() + str1.ToString();
        }
    }
}