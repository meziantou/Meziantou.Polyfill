using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static int Count(ReadOnlySpan<char> input, string pattern, RegexOptions options) => new Regex(pattern, options).Matches(input.ToString()).Count;
    }
}
