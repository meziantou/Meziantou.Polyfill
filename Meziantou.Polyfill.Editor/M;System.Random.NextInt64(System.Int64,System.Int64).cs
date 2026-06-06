using System;
// when M:System.Random.NextInt64
// when M:System.Random.NextInt64(System.Int64)
static partial class PolyfillExtensions
{
    public static long NextInt64(this Random target, long minValue, long maxValue)
    {
        if (minValue >= maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue));

        var range = unchecked((ulong)(maxValue - minValue));
        var limit = ulong.MaxValue - (ulong.MaxValue % range);
        ulong sample;
        do
        {
            sample = Helpers.NextUInt64(target);
        } while (sample >= limit);
        return unchecked((long)((ulong)minValue + sample % range));
    }
}

file class Helpers
{
    internal static ulong NextUInt64(Random target)
    {
        var bytes = new byte[8];
        target.NextBytes(bytes);
        return BitConverter.ToUInt64(bytes, 0);
    }
}