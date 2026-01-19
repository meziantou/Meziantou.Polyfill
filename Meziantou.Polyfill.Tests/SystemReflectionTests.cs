using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemReflectionTests
{
    [Fact]
    public void Type_GetConstructor()
    {
        Assert.NotNull(typeof(Exception).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[]
            {
                typeof(SerializationInfo),
                typeof(StreamingContext)
            })
        );
        Assert.NotNull(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
        Assert.Null(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { typeof(IntPtr) }));
    }

    [Fact]
    public void Type_GetMethod()
    {
        Assert.NotNull(typeof(Exception).GetMethod("get_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
        Assert.Null(typeof(Exception).GetMethod("set_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
    }

    [Fact]
    public void Type_IsAssignableTo()
    {
        Assert.True(typeof(string).IsAssignableTo(typeof(object)));
        Assert.True(typeof(string).IsAssignableTo(typeof(string)));
        Assert.False(typeof(string).IsAssignableTo(typeof(int)));
    }

#if NET6_0_OR_GREATER
    [Fact]
    public void Type_GetMethod_WithGenericParameterCount()
    {
        // Get a generic method with specific generic parameter count using full overload
        var method = typeof(Enumerable).GetMethod("Select", 2, BindingFlags.Static | BindingFlags.Public, binder: null, new Type[] { typeof(IEnumerable<>).MakeGenericType(Type.MakeGenericMethodParameter(0)), typeof(Func<,>).MakeGenericType(Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(1)) }, modifiers: null);
        Assert.NotNull(method);
        Assert.Equal(2, method!.GetGenericArguments().Length);
    }

    [Fact]
    public void Type_GetMethod_WithGenericParameterCount_FullSignature()
    {
        var method = typeof(Enumerable).GetMethod("Select", 2, BindingFlags.Static | BindingFlags.Public, binder: null, new Type[] { typeof(IEnumerable<>).MakeGenericType(Type.MakeGenericMethodParameter(0)), typeof(Func<,>).MakeGenericType(Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(1)) }, modifiers: null);
        Assert.NotNull(method);
    }
#endif

    [Fact]
    public void Type_IsGenericMethodParameter()
    {
        var method = typeof(Array).GetMethod("Empty", BindingFlags.Static | BindingFlags.Public)!;
        Assert.True(method.ContainsGenericParameters);
        Assert.True(method.GetGenericArguments()[0].IsGenericMethodParameter);
        Assert.False(typeof(int).IsGenericMethodParameter);
        Assert.False(typeof(List<>).GetGenericArguments()[0].IsGenericMethodParameter);
    }

    [Fact]
    public void MethodInfo_CreateDelegate_Generic()
    {
        var method = typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!;
        var del = method.CreateDelegate<Func<string, string>>();
        Assert.Equal("HELLO", del("hello"));
    }

    [Fact]
    public void MethodInfo_CreateDelegate_Generic_WithTarget()
    {
        var method = typeof(string).GetMethod("ToUpper", Type.EmptyTypes)!;
        var del = method.CreateDelegate<Func<string>>("hello");
        Assert.Equal("HELLO", del());
    }
}
