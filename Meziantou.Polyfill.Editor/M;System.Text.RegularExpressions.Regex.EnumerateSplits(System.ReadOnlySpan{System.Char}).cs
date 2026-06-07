using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input) => new(regex, input.ToString(), 0, 0);
}
