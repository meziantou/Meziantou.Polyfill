using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static MethodInfo? GetMethod(this Type type, string name, int genericParameterCount, BindingFlags bindingFlags, Binder? binder, Type[] types, ParameterModifier[]? modifiers)
    {
        foreach (var method in type.GetMethods(bindingFlags))
        {
            if (method.Name != name)
                continue;

            if (method.GetGenericArguments().Length != genericParameterCount)
                continue;

            var parameters = method.GetParameters();
            if (parameters.Length != types.Length)
                continue;

            var match = true;
            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType != types[i])
                {
                    match = false;
                    break;
                }
            }

            if (match)
                return method;
        }

        return null;
    }
}
