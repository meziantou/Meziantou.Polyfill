using System;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool IsAssignableTo(this Type target, [NotNullWhen(true)] Type? targetType)
    {
        return targetType?.IsAssignableFrom(target) ?? false;
    }
}