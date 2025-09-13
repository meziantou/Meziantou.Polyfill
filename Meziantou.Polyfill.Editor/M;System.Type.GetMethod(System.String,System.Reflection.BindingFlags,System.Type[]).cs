using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static MethodInfo? GetMethod(this Type type, string name, BindingFlags bindingFlags, Type[] parameterTypes) =>
        type.GetMethod(name, bindingFlags, null, parameterTypes, null);
}
