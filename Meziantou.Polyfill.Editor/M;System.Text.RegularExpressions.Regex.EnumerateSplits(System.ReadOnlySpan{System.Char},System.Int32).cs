using System;
using System.Text.RegularExpressions;

static partial class PolyfillExtensions
{
    public static ValueSplitEnumerator EnumerateSplits(this Regex regex, ReadOnlySpan<char> input, int count) => new(regex, input.ToString(), count, 0);
}
