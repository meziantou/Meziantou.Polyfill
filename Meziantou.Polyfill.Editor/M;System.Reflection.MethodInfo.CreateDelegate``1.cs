using System;
using System.Reflection;

static partial class PolyfillExtensions
{
    public static T CreateDelegate<T>(this MethodInfo methodInfo) where T : Delegate
    {
        return (T)methodInfo.CreateDelegate(typeof(T));
    }
}
