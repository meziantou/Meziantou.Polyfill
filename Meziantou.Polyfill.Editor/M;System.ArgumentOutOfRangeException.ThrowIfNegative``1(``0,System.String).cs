using System;
using System.Numerics;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(ArgumentOutOfRangeException)
    {
        public static void ThrowIfNegative<T>(T value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where T : INumberBase<T>
        {
            if (T.IsNegative(value))
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be a non-negative value.");
        }
    }
}
