// when M:System.Security.Cryptography.RandomNumberGenerator.GetItems``1(System.ReadOnlySpan{``0},System.Int32)
using System;

partial class PolyfillExtensions
{
    extension(System.Security.Cryptography.RandomNumberGenerator)
    {
        public static string GetString(System.ReadOnlySpan<char> choices, int length)
        {
            if (choices.IsEmpty)
                throw new System.ArgumentException("choices cannot be empty.", nameof(choices));

            if (length < 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(length), length, "must not be negative.");
            }

            if (length == 0)
                return string.Empty;

            char[] result = new char[length];
            GetItems(choices, new Span<char>(result));
            return new string(result);
        }
    }
}
