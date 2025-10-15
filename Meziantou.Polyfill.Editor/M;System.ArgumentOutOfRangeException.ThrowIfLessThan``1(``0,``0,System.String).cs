using System;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(ArgumentOutOfRangeException)
    {
        public static void ThrowIfLessThan<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(other) < 0)
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must not be less than '{other}'.");
        }
    }
}