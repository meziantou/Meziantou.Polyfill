// when M:System.Type.GetMethod(System.String,System.Int32,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])
using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static MethodInfo? GetMethod(this Type type, string name, int genericParameterCount, BindingFlags bindingFlags, Type[] types)
    {
        return type.GetMethod(name, genericParameterCount, bindingFlags, binder: null, types, modifiers: null);
    }
}
