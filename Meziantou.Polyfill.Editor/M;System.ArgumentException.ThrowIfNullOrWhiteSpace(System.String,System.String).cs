using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(ArgumentException)
    {
        public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("String cannot be empty or whitespace.", paramName);
            }
        }
    }
}