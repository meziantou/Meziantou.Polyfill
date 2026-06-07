using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static int Count(ReadOnlySpan<char> input, string pattern) => new Regex(pattern).Matches(input.ToString()).Count;
    }
}
