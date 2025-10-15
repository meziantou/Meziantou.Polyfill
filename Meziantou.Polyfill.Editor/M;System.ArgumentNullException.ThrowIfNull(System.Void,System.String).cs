// XML-DOC: M:System.ArgumentNullException.ThrowIfNull(System.Void*,System.String)
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(ArgumentNullException)
    {
        public static unsafe void ThrowIfNull([NotNull] void* argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}