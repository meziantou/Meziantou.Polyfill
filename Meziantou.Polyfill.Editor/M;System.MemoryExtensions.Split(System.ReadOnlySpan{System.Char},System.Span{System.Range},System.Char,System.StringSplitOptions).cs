using System;

static partial class PolyfillExtensions
{
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        var removeEmptyEntries = (options & StringSplitOptions.RemoveEmptyEntries) != 0;
#if NET5_0_OR_GREATER
        var trimEntries = (options & StringSplitOptions.TrimEntries) != 0;
#else
        var trimEntries = ((int)options & 2) != 0; // StringSplitOptions.TrimEntries = 2
#endif

        var count = 0;
        var index = 0;

        while (index <= source.Length && count < destination.Length)
        {
            var separatorIndex = source.Slice(index).IndexOf(separator);
            
            if (separatorIndex < 0)
            {
                // No more separators found
                var finalSegment = source.Slice(index);
                if (trimEntries)
                {
                    var trimmedStart = index;
                    var trimmedEnd = source.Length;
                    
                    while (trimmedStart < source.Length && char.IsWhiteSpace(source[trimmedStart]))
                        trimmedStart++;
                    while (trimmedEnd > trimmedStart && char.IsWhiteSpace(source[trimmedEnd - 1]))
                        trimmedEnd--;

                    if (trimmedStart < trimmedEnd || !removeEmptyEntries)
                    {
                        destination[count++] = new Range(trimmedStart, trimmedEnd);
                    }
                }
                else if (index < source.Length || !removeEmptyEntries)
                {
                    destination[count++] = new Range(index, source.Length);
                }
                break;
            }

            var segmentStart = index;
            var segmentEnd = index + separatorIndex;

            if (trimEntries)
            {
                while (segmentStart < segmentEnd && char.IsWhiteSpace(source[segmentStart]))
                    segmentStart++;
                while (segmentEnd > segmentStart && char.IsWhiteSpace(source[segmentEnd - 1]))
                    segmentEnd--;
            }

            if (segmentStart < segmentEnd || !removeEmptyEntries)
            {
                destination[count++] = new Range(segmentStart, segmentEnd);
            }

            index += separatorIndex + 1;
        }

        return count;
    }
}
