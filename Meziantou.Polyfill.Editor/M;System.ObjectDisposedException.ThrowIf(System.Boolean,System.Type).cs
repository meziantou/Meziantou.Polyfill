using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    extension(ObjectDisposedException)
    {
       [StackTraceHidden]
        public static void ThrowIf([DoesNotReturnIf(true)] bool condition, Type type)
        {
            if (condition)
            {
                throw new ObjectDisposedException(type?.FullName);
            }
        }
    }
}