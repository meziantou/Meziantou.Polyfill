using System;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Concat(ReadOnlySpan<char> str0, ReadOnlySpan<char> str1, ReadOnlySpan<char> str2, ReadOnlySpan<char> str3)
        {
            return str0.ToString() + str1.ToString() + str2.ToString() + str3.ToString();
        }
    }
}