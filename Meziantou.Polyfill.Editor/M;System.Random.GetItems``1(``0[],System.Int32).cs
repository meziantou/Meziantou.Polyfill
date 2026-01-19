// when M:System.Random.GetItems``1(System.ReadOnlySpan{``0},System.Int32)
using System;

partial class PolyfillExtensions
{
    public static T[] GetItems<T>(this Random random, T[] choices, int length)
    {
        ArgumentNullException.ThrowIfNull(random);
        ArgumentNullException.ThrowIfNull(choices);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        return GetItems(random, new ReadOnlySpan<T>(choices), length);
    }
}
