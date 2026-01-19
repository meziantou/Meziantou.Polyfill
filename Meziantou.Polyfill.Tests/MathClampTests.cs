using Xunit;

namespace Meziantou.Polyfill.Tests;

public class MathClampTests
{
    [Fact]
    public void Clamp_Byte_InRange()
    {
        Assert.Equal((byte)5, Math.Clamp((byte)5, (byte)0, (byte)10));
    }

    [Fact]
    public void Clamp_Byte_BelowRange()
    {
        Assert.Equal((byte)10, Math.Clamp((byte)5, (byte)10, (byte)20));
    }

    [Fact]
    public void Clamp_Byte_AboveRange()
    {
        Assert.Equal((byte)20, Math.Clamp((byte)25, (byte)10, (byte)20));
    }

    [Fact]
    public void Clamp_Byte_MinEqualsMax()
    {
        Assert.Equal((byte)10, Math.Clamp((byte)5, (byte)10, (byte)10));
        Assert.Equal((byte)10, Math.Clamp((byte)10, (byte)10, (byte)10));
        Assert.Equal((byte)10, Math.Clamp((byte)15, (byte)10, (byte)10));
    }

    [Fact]
    public void Clamp_Byte_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((byte)5, (byte)10, (byte)5));
    }

    [Fact]
    public void Clamp_SByte_InRange()
    {
        Assert.Equal((sbyte)5, Math.Clamp((sbyte)5, (sbyte)-10, (sbyte)10));
    }

    [Fact]
    public void Clamp_SByte_BelowRange()
    {
        Assert.Equal((sbyte)-10, Math.Clamp((sbyte)-20, (sbyte)-10, (sbyte)10));
    }

    [Fact]
    public void Clamp_SByte_AboveRange()
    {
        Assert.Equal((sbyte)10, Math.Clamp((sbyte)20, (sbyte)-10, (sbyte)10));
    }

    [Fact]
    public void Clamp_SByte_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((sbyte)0, (sbyte)10, (sbyte)5));
    }

    [Fact]
    public void Clamp_Int16_InRange()
    {
        Assert.Equal((short)5, Math.Clamp((short)5, (short)-10, (short)10));
    }

    [Fact]
    public void Clamp_Int16_BelowRange()
    {
        Assert.Equal((short)-10, Math.Clamp((short)-20, (short)-10, (short)10));
    }

    [Fact]
    public void Clamp_Int16_AboveRange()
    {
        Assert.Equal((short)10, Math.Clamp((short)20, (short)-10, (short)10));
    }

    [Fact]
    public void Clamp_Int16_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((short)0, (short)10, (short)5));
    }

    [Fact]
    public void Clamp_UInt16_InRange()
    {
        Assert.Equal((ushort)5, Math.Clamp((ushort)5, (ushort)0, (ushort)10));
    }

    [Fact]
    public void Clamp_UInt16_BelowRange()
    {
        Assert.Equal((ushort)10, Math.Clamp((ushort)5, (ushort)10, (ushort)20));
    }

    [Fact]
    public void Clamp_UInt16_AboveRange()
    {
        Assert.Equal((ushort)20, Math.Clamp((ushort)25, (ushort)10, (ushort)20));
    }

    [Fact]
    public void Clamp_UInt16_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((ushort)5, (ushort)10, (ushort)5));
    }

    [Fact]
    public void Clamp_Int32_InRange()
    {
        Assert.Equal(5, Math.Clamp(5, -10, 10));
    }

    [Fact]
    public void Clamp_Int32_BelowRange()
    {
        Assert.Equal(-10, Math.Clamp(-20, -10, 10));
    }

    [Fact]
    public void Clamp_Int32_AboveRange()
    {
        Assert.Equal(10, Math.Clamp(20, -10, 10));
    }

    [Fact]
    public void Clamp_Int32_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(0, 10, 5));
    }

    [Fact]
    public void Clamp_UInt32_InRange()
    {
        Assert.Equal(5u, Math.Clamp(5u, 0u, 10u));
    }

    [Fact]
    public void Clamp_UInt32_BelowRange()
    {
        Assert.Equal(10u, Math.Clamp(5u, 10u, 20u));
    }

    [Fact]
    public void Clamp_UInt32_AboveRange()
    {
        Assert.Equal(20u, Math.Clamp(25u, 10u, 20u));
    }

    [Fact]
    public void Clamp_UInt32_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(5u, 10u, 5u));
    }

    [Fact]
    public void Clamp_Int64_InRange()
    {
        Assert.Equal(5L, Math.Clamp(5L, -10L, 10L));
    }

    [Fact]
    public void Clamp_Int64_BelowRange()
    {
        Assert.Equal(-10L, Math.Clamp(-20L, -10L, 10L));
    }

    [Fact]
    public void Clamp_Int64_AboveRange()
    {
        Assert.Equal(10L, Math.Clamp(20L, -10L, 10L));
    }

    [Fact]
    public void Clamp_Int64_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(0L, 10L, 5L));
    }

    [Fact]
    public void Clamp_UInt64_InRange()
    {
        Assert.Equal(5ul, Math.Clamp(5ul, 0ul, 10ul));
    }

    [Fact]
    public void Clamp_UInt64_BelowRange()
    {
        Assert.Equal(10ul, Math.Clamp(5ul, 10ul, 20ul));
    }

    [Fact]
    public void Clamp_UInt64_AboveRange()
    {
        Assert.Equal(20ul, Math.Clamp(25ul, 10ul, 20ul));
    }

    [Fact]
    public void Clamp_UInt64_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(5ul, 10ul, 5ul));
    }

    [Fact]
    public void Clamp_IntPtr_InRange()
    {
        Assert.Equal((nint)5, Math.Clamp((nint)5, (nint)(-10), (nint)10));
    }

    [Fact]
    public void Clamp_IntPtr_BelowRange()
    {
        Assert.Equal((nint)(-10), Math.Clamp((nint)(-20), (nint)(-10), (nint)10));
    }

    [Fact]
    public void Clamp_IntPtr_AboveRange()
    {
        Assert.Equal((nint)10, Math.Clamp((nint)20, (nint)(-10), (nint)10));
    }

    [Fact]
    public void Clamp_IntPtr_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((nint)0, (nint)10, (nint)5));
    }

    [Fact]
    public void Clamp_UIntPtr_InRange()
    {
        Assert.Equal((nuint)5, Math.Clamp((nuint)5, (nuint)0, (nuint)10));
    }

    [Fact]
    public void Clamp_UIntPtr_BelowRange()
    {
        Assert.Equal((nuint)10, Math.Clamp((nuint)5, (nuint)10, (nuint)20));
    }

    [Fact]
    public void Clamp_UIntPtr_AboveRange()
    {
        Assert.Equal((nuint)20, Math.Clamp((nuint)25, (nuint)10, (nuint)20));
    }

    [Fact]
    public void Clamp_UIntPtr_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp((nuint)5, (nuint)10, (nuint)5));
    }

    [Fact]
    public void Clamp_Single_InRange()
    {
        Assert.Equal(5.5f, Math.Clamp(5.5f, -10.5f, 10.5f));
    }

    [Fact]
    public void Clamp_Single_BelowRange()
    {
        Assert.Equal(-10.5f, Math.Clamp(-20.5f, -10.5f, 10.5f));
    }

    [Fact]
    public void Clamp_Single_AboveRange()
    {
        Assert.Equal(10.5f, Math.Clamp(20.5f, -10.5f, 10.5f));
    }

    [Fact]
    public void Clamp_Single_WithNaN()
    {
        Assert.Equal(float.NaN, Math.Clamp(float.NaN, -10.5f, 10.5f));
    }

    [Fact]
    public void Clamp_Single_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(0f, 10f, 5f));
    }

    [Fact]
    public void Clamp_Double_InRange()
    {
        Assert.Equal(5.5, Math.Clamp(5.5, -10.5, 10.5));
    }

    [Fact]
    public void Clamp_Double_BelowRange()
    {
        Assert.Equal(-10.5, Math.Clamp(-20.5, -10.5, 10.5));
    }

    [Fact]
    public void Clamp_Double_AboveRange()
    {
        Assert.Equal(10.5, Math.Clamp(20.5, -10.5, 10.5));
    }

    [Fact]
    public void Clamp_Double_WithNaN()
    {
        Assert.Equal(double.NaN, Math.Clamp(double.NaN, -10.5, 10.5));
    }

    [Fact]
    public void Clamp_Double_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(0.0, 10.0, 5.0));
    }

    [Fact]
    public void Clamp_Decimal_InRange()
    {
        Assert.Equal(5.5m, Math.Clamp(5.5m, -10.5m, 10.5m));
    }

    [Fact]
    public void Clamp_Decimal_BelowRange()
    {
        Assert.Equal(-10.5m, Math.Clamp(-20.5m, -10.5m, 10.5m));
    }

    [Fact]
    public void Clamp_Decimal_AboveRange()
    {
        Assert.Equal(10.5m, Math.Clamp(20.5m, -10.5m, 10.5m));
    }

    [Fact]
    public void Clamp_Decimal_ThrowsWhenMaxLessThanMin()
    {
        Assert.Throws<ArgumentException>(() => Math.Clamp(0m, 10m, 5m));
    }
}
