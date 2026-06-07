using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static bool IsMatch(ReadOnlySpan<char> input, string pattern) => Regex.IsMatch(input.ToString(), pattern);
    }
}
