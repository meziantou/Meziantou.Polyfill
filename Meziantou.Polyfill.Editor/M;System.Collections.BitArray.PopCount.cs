using System.Collections;

static partial class PolyfillExtensions
{
    public static int PopCount(this BitArray value)
    {
        var count = 0;
        for (var index = 0; index < value.Length; index++)
        {
            if (value[index])
                count++;
        }

        return count;
    }
}
