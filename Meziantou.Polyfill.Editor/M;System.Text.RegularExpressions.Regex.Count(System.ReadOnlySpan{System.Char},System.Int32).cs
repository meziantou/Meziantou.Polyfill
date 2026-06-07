using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static int Count(this Regex regex, ReadOnlySpan<char> input, int startat) => regex.Matches(input.ToString(), startat).Count;
}
