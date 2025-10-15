using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(ArgumentOutOfRangeException)
    {
        public static void ThrowIfNotEqual<T>(T value, T other, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(value, other))
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must not be equal to '{other}'.");
        }
    }
}