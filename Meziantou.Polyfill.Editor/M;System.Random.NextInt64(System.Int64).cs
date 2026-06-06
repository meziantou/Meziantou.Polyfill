using System;

static partial class PolyfillExtensions
{
    public static long NextInt64(this Random target, long maxValue)
    {
        if (maxValue < 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        return maxValue == 0 ? 0 : target.NextInt64(0, maxValue);
    }
}
