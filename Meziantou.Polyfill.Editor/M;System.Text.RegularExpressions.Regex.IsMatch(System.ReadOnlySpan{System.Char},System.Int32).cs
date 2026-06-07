using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static bool IsMatch(this Regex regex, ReadOnlySpan<char> input, int startat) => regex.IsMatch(input.ToString(), startat);
}
