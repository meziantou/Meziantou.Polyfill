using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static bool IsMatch(ReadOnlySpan<char> input, string pattern, RegexOptions options) => Regex.IsMatch(input.ToString(), pattern, options);
    }
}
