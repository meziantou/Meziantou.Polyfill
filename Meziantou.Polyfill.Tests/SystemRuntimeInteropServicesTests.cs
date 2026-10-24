using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemRuntimeInteropServicesTests
{
    [Fact]
    public void MemoryMarshal_Write_In()
    {
        var buffer = new byte[sizeof(int) + 1];
        var value = 0x12345678;

        // 'MemoryMarshal.Write'/'MemoryMarshal.Read' are caller-unsafe under the updated memory safety rules
        unsafe
        {
            MemoryMarshal.Write(buffer.AsSpan(1), in value);
            Assert.Equal(value, MemoryMarshal.Read<int>(buffer.AsSpan(1)));
            Assert.Equal(0, buffer[0]);

            MemoryMarshal.Write(buffer.AsSpan(), 42);
            Assert.Equal(42, MemoryMarshal.Read<int>(buffer));
        }
    }

    [Fact]
    public void MemoryMarshal_Write_In_DestinationTooSmall()
    {
        var value = 42L;

        // 'MemoryMarshal.Write' is caller-unsafe under the updated memory safety rules
        unsafe
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => MemoryMarshal.Write(new byte[sizeof(long) - 1], in value));
        }
    }
}
