using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

    [Fact]
    public void MemberInfo_HasSameMetadataDefinitionAs()
    {
        var member = typeof(string).GetMethod(nameof(string.ToString), Type.EmptyTypes)!;
        Assert.True(member.HasSameMetadataDefinitionAs(member));
        Assert.False(member.HasSameMetadataDefinitionAs(typeof(object).GetMethod(nameof(ToString), Type.EmptyTypes)!));
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

    [Fact]
    public void NullabilityInfoContext_Create_NullableProperty()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NullableString))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
        Assert.Equal(NullabilityState.Nullable, info.WriteState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NonNullableProperty()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NonNullString))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
        Assert.Equal(NullabilityState.NotNull, info.WriteState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_ValueTypeProperty()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.IntValue))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NullableValueTypeProperty()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NullableInt))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_Field()
    {
        var context = new NullabilityInfoContext();
        var field = typeof(NullabilityTestHelper).GetField(nameof(NullabilityTestHelper.NullableField))!;
        var info = context.Create(field);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_Parameter()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.MethodWithNullableParam))!;
        var param = method.GetParameters()[0];
        var info = context.Create(param);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_GenericTypeArguments()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.ListOfNullableStrings))!;
        var info = context.Create(property);
        Assert.Single(info.GenericTypeArguments);
        Assert.Equal(NullabilityState.Nullable, info.GenericTypeArguments[0].ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NestedGenericType()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NestedGeneric))!;
        var info = context.Create(property);

        Assert.Equal(2, info.GenericTypeArguments.Length);
        Assert.Equal(NullabilityState.NotNull, info.GenericTypeArguments[0].ReadState);
        Assert.Equal(NullabilityState.Nullable, info.GenericTypeArguments[1].ReadState);
        Assert.Single(info.GenericTypeArguments[1].GenericTypeArguments);
        Assert.Equal(NullabilityState.Nullable, info.GenericTypeArguments[1].GenericTypeArguments[0].ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_MaybeNullAttribute()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.MethodWithMaybeNullReturn))!;
        var info = context.Create(method.ReturnParameter);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NotNullAttribute()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.MethodWithNotNullParam))!;
        var param = method.GetParameters()[0];
        var info = context.Create(param);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_AllowNullAttribute()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.AllowNullProperty))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
        Assert.Equal(NullabilityState.Nullable, info.WriteState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_DisallowNullAttribute()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.DisallowNullProperty))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
        Assert.Equal(NullabilityState.NotNull, info.WriteState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_MaybeNullWhenAttribute()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.TryGetValue))!;
        var param = method.GetParameters()[0];
        var info = context.Create(param);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_Event()
    {
        var context = new NullabilityInfoContext();
        var eventInfo = typeof(NullabilityTestHelper).GetEvent(nameof(NullabilityTestHelper.MyEvent))!;
        var info = context.Create(eventInfo);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NullableArrayElement()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NullableStringArray))!;
        var info = context.Create(property);
        Assert.NotNull(info.ElementType);
        Assert.Equal(NullabilityState.Nullable, info.ElementType!.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NonNullableArrayElement()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.NonNullStringArray))!;
        var info = context.Create(property);
        Assert.NotNull(info.ElementType);
        Assert.Equal(NullabilityState.NotNull, info.ElementType!.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_ArrayOfGeneric()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityTestHelper).GetProperty(nameof(NullabilityTestHelper.ArrayOfGeneric))!;
        var info = context.Create(property);
        Assert.NotNull(info.ElementType);
        Assert.Single(info.ElementType!.GenericTypeArguments);
        Assert.Equal(NullabilityState.Nullable, info.ElementType.GenericTypeArguments[0].ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NonNullableField()
    {
        var context = new NullabilityInfoContext();
        var field = typeof(NullabilityTestHelper).GetField(nameof(NullabilityTestHelper.NonNullableField))!;
        var info = context.Create(field);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NonNullableParameter()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.MethodWithNonNullParam))!;
        var param = method.GetParameters()[0];
        var info = context.Create(param);
        Assert.Equal(NullabilityState.NotNull, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_NullableReturnType()
    {
        var context = new NullabilityInfoContext();
        var method = typeof(NullabilityTestHelper).GetMethod(nameof(NullabilityTestHelper.MethodReturningNullable))!;
        var info = context.Create(method.ReturnParameter);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    [Fact]
    public void NullabilityInfoContext_Create_GenericTypeInheritance()
    {
        var context = new NullabilityInfoContext();
        var property = typeof(NullabilityDerivedHelper).GetProperty(nameof(NullabilityDerivedHelper.Value))!;
        var info = context.Create(property);
        Assert.Equal(NullabilityState.Nullable, info.ReadState);
    }

    private sealed class NullabilityTestHelper
    {
        public string NonNullableField = "";
#pragma warning disable CA1805
        public string? NullableField = null;
#pragma warning restore CA1805

        public string? NullableString { get; set; }
        public string NonNullString { get; set; } = "";
        public int IntValue { get; set; }
        public int? NullableInt { get; set; }
        public List<string?> ListOfNullableStrings { get; set; } = new();
        public Dictionary<string, List<string?>?> NestedGeneric { get; set; } = new();
        public string?[] NullableStringArray { get; set; } = Array.Empty<string?>();
        public string[] NonNullStringArray { get; set; } = Array.Empty<string>();
        public List<string?>[] ArrayOfGeneric { get; set; } = Array.Empty<List<string?>>();

        [AllowNull]
        public string AllowNullProperty { get; set; } = "";

        [DisallowNull]
        public string? DisallowNullProperty { get; set; } = "";

        public static event EventHandler MyEvent { add { } remove { } }

        public static string? MethodReturningNullable() => null;

        [return: MaybeNull]
        public static string MethodWithMaybeNullReturn() => "";

#pragma warning disable IDE0060
        public static void MethodWithNullableParam(string? value) { }
        public static void MethodWithNonNullParam(string value) { }
#pragma warning restore IDE0060

#pragma warning disable IDE0370
        public static void MethodWithNotNullParam([NotNull] string? value) { _ = value!.Length; }
        public static bool TryGetValue([MaybeNullWhen(false)] out string value) { value = null!; return false; }
#pragma warning restore IDE0370
    }

    private class NullabilityBaseHelper<T>
    {
        public T Value { get; set; } = default!;
    }

    private sealed class NullabilityDerivedHelper : NullabilityBaseHelper<string?>
    {
    }
}
