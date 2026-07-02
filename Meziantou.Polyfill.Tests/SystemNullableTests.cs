using System;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class SystemNullableTests
{
    [Fact]
    public void Nullable_GetValueRefOrDefaultRef_WithValue_ReturnsReferenceToUnderlyingStorage()
    {
        int? value = 1;

        ref readonly var valueRef = ref Nullable.GetValueRefOrDefaultRef(in value);

        Assert.Equal(1, valueRef);

        value = 42;

        Assert.Equal(42, valueRef);
    }

    [Fact]
    public void Nullable_GetValueRefOrDefaultRef_WithoutValue_ReturnsReferenceToUnderlyingStorage()
    {
        int? value = default;

        ref readonly var valueRef = ref Nullable.GetValueRefOrDefaultRef(in value);

        Assert.Equal(0, valueRef);

        value = 42;

        Assert.Equal(42, valueRef);
    }
}
