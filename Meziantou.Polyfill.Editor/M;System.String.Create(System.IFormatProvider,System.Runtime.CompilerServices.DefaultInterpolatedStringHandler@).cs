using System;
using System.Runtime.CompilerServices;

static partial class PolyfillExtensions
{
    extension(string)
    {
        public static string Create(IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(provider))] ref DefaultInterpolatedStringHandler handler) =>
            handler.ToStringAndClear();
    }
}