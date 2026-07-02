using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class SystemNullableTests
{
    [Fact]
    public void Nullable_GetValueRefOrDefaultRef_WithValue_ReturnsReferenceToUnderlyingStorage()
    {
        int? value = 1;

        ref readonly var valueRef = ref Nullable.GetValueRefOrDefaultRef(in value);
        ref var expectedRef = ref Unsafe.As<int?, NullableValueStorage<int>>(ref value).Value;

        Assert.True(value.HasValue);
        Assert.Equal(1, value.Value);
        Assert.Equal(1, valueRef);
        Assert.True(Unsafe.AreSame(ref expectedRef, ref Unsafe.AsRef(in valueRef)));

        value = 42;

        Assert.True(value.HasValue);
        Assert.Equal(42, value.Value);
        Assert.Equal(42, valueRef);
    }

    [Fact]
    public void Nullable_GetValueRefOrDefaultRef_WithoutValue_ReturnsReferenceToUnderlyingStorage()
    {
        int? value = default;

        ref readonly var valueRef = ref Nullable.GetValueRefOrDefaultRef(in value);
        ref var expectedRef = ref Unsafe.As<int?, NullableValueStorage<int>>(ref value).Value;

        Assert.False(value.HasValue);
        Assert.Equal(0, valueRef);
        Assert.True(Unsafe.AreSame(ref expectedRef, ref Unsafe.AsRef(in valueRef)));

        value = 42;

        Assert.True(value.HasValue);
        Assert.Equal(42, value.Value);
        Assert.Equal(42, valueRef);
    }
}

#pragma warning disable CS0649
file struct NullableValueStorage<T>
    where T : struct
{
    public bool HasValue;
    public T Value;
}
#pragma warning restore CS0649
