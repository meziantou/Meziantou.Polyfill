using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions_Regex
{
    extension(Regex)
    {
        public static int Count(string input, string pattern, RegexOptions options, TimeSpan matchTimeout) => new Regex(pattern, options, matchTimeout).Matches(input).Count;
    }
}
