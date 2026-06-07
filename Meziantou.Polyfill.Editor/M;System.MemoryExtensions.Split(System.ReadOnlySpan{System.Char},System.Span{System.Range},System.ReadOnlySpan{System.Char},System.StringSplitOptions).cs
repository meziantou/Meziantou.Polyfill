using System;

static partial class PolyfillExtensions
{
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None) =>
        SpanSplitImplementation.Split(source, destination, separator.ToString(), any: false, options);
}

file static class SpanSplitImplementation
{
    private const StringSplitOptions TrimEntries = (StringSplitOptions)2;

    public static int Split(ReadOnlySpan<char> source, Span<Range> destination, string separator, bool any, StringSplitOptions options) =>
        Split(source, destination, [separator], any, options);

    private static int Split(ReadOnlySpan<char> source, Span<Range> destination, string[] separators, bool any, StringSplitOptions options)
    {
        if ((options & ~(StringSplitOptions.RemoveEmptyEntries | TrimEntries)) != 0)
            throw new ArgumentException("Invalid StringSplitOptions value.", nameof(options));
        if (destination.IsEmpty)
            return 0;

        var sourceString = source.ToString();
        var count = 0;
        var start = 0;
        while (start <= sourceString.Length)
        {
            var separatorIndex = -1;
            var separatorLength = 0;
            foreach (var separator in separators)
            {
                if (string.IsNullOrEmpty(separator))
                    continue;

                var index = any ? sourceString.IndexOfAny(separator.ToCharArray(), start) : sourceString.IndexOf(separator, start, StringComparison.Ordinal);
                if (index >= 0 && (separatorIndex < 0 || index < separatorIndex))
                {
                    separatorIndex = index;
                    separatorLength = any ? 1 : separator.Length;
                }
            }

            var end = separatorIndex < 0 ? sourceString.Length : separatorIndex;
            var itemStart = start;
            if ((options & TrimEntries) != 0)
            {
                while (itemStart < end && char.IsWhiteSpace(sourceString[itemStart]))
                    itemStart++;
                while (end > itemStart && char.IsWhiteSpace(sourceString[end - 1]))
                    end--;
            }

            if ((options & StringSplitOptions.RemoveEmptyEntries) == 0 || itemStart != end)
            {
                if (count == destination.Length - 1 && separatorIndex >= 0)
                {
                    destination[count++] = new Range(itemStart, sourceString.Length);
                    break;
                }

                destination[count++] = new Range(itemStart, end);
                if (count == destination.Length)
                    break;
            }

            if (separatorIndex < 0)
                break;
            start = separatorIndex + separatorLength;
        }

        return count;
    }
}
