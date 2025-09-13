using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static ConstructorInfo? GetConstructor(this Type type, BindingFlags bindingFlags, Type[] parameterTypes) =>
        type.GetConstructor(bindingFlags, binder: null, parameterTypes, modifiers: null);
}
