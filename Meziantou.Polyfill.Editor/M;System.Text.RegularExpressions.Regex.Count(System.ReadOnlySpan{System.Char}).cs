using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static int Count(this Regex regex, ReadOnlySpan<char> input) => regex.Matches(input.ToString()).Count;
}
