// when M:System.Security.Cryptography.RandomNumberGenerator.GetItems``1(System.ReadOnlySpan{``0},System.Span{``0})
using System;

partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static T[] GetItems<T>(System.ReadOnlySpan<T> choices, int length)
        {
            if (choices.IsEmpty)
                throw new System.ArgumentException("choices cannot be empty.", nameof(choices));

            System.ArgumentOutOfRangeException.ThrowIfNegative(length);

            T[] result = new T[length];
            GetItems(choices, new Span<T>(result));
            return result;
        }
    }
}
