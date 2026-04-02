// when M:System.Random.GetItems``1(System.ReadOnlySpan{``0},System.Int32)
using System;

partial class PolyfillExtensions
{
    public static T[] GetItems<T>(this Random random, T[] choices, int length)
    {
        if (random is null)
        {
            throw new ArgumentNullException(nameof(random));
        }

        if (choices is null)
        {
            throw new ArgumentNullException(nameof(choices));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "must not be negative.");
        }

        return GetItems(random, new ReadOnlySpan<T>(choices), length);
    }
}
