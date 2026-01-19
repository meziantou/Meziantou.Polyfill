using System;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class RandomNumberGeneratorTests
{
#if !(NETCOREAPP2_1_OR_GREATER || NET5_0_OR_GREATER)
    [Fact]
    public void Fill_Span_FillsWithRandomBytes()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> buffer = new byte[100];
        rng.Fill(buffer);

        // Check that not all bytes are zero (very unlikely with random data)
        Assert.Contains(buffer.ToArray(), b => b != 0);
    }

    [Fact]
    public void Fill_EmptySpan_DoesNotThrow()
    {
        using var rng = RandomNumberGenerator.Create();
        Span<byte> buffer = Span<byte>.Empty;
        rng.Fill(buffer);
    }
#endif

    [Fact]
    public void GetBytes_Static_CreatesArray()
    {
        byte[] result = RandomNumberGenerator.GetBytes(100);
        Assert.Equal(100, result.Length);
        Assert.Contains(result, b => b != 0);
    }

    [Fact]
    public void GetBytes_ZeroLength_ReturnsEmptyArray()
    {
        byte[] result = RandomNumberGenerator.GetBytes(0);
        Assert.Empty(result);
    }

    [Fact]
    public void GetBytes_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetBytes(-1));
    }

    [Fact]
    public void GetInt32_TwoParams_ReturnsValueInRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int value = RandomNumberGenerator.GetInt32(10, 20);
            Assert.InRange(value, 10, 19);
        }
    }

    [Fact]
    public void GetInt32_TwoParams_NegativeRange_Works()
    {
        for (int i = 0; i < 100; i++)
        {
            int value = RandomNumberGenerator.GetInt32(-20, -10);
            Assert.InRange(value, -20, -11);
        }
    }

    [Fact]
    public void GetInt32_TwoParams_InvalidRange_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => RandomNumberGenerator.GetInt32(10, 10));
        Assert.Throws<ArgumentException>(() => RandomNumberGenerator.GetInt32(10, 5));
    }

    [Fact]
    public void GetInt32_OneParam_ReturnsValueInRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int value = RandomNumberGenerator.GetInt32(20);
            Assert.InRange(value, 0, 19);
        }
    }

    [Fact]
    public void GetInt32_OneParam_ZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetInt32(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => RandomNumberGenerator.GetInt32(-1));
    }

    [Fact]
    public void GetHexString_Int_CreatesHexString()
    {
        string result = RandomNumberGenerator.GetHexString(20);
        Assert.Equal(20, result.Length);
        Assert.All(result, c => Assert.True((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F')));
    }

    [Fact]
    public void GetHexString_Int_Lowercase_CreatesLowercaseHexString()
    {
        string result = RandomNumberGenerator.GetHexString(20, lowercase: true);
        Assert.Equal(20, result.Length);
        Assert.All(result, c => Assert.True((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f')));
    }

    [Fact]
    public void GetHexString_Int_ZeroLength_ReturnsEmptyString()
    {
        string result = RandomNumberGenerator.GetHexString(0);
        Assert.Empty(result);
    }

    [Fact]
    public void GetHexString_Span_FillsWithHexCharacters()
    {
        Span<char> buffer = new char[20];
        RandomNumberGenerator.GetHexString(buffer);
        Assert.All(buffer.ToArray(), c => Assert.True((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F')));
    }

    [Fact]
    public void GetHexString_Span_Lowercase_FillsWithLowercaseHex()
    {
        Span<char> buffer = new char[20];
        RandomNumberGenerator.GetHexString(buffer, lowercase: true);
        Assert.All(buffer.ToArray(), c => Assert.True((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f')));
    }

    [Fact]
    public void GetHexString_EmptySpan_DoesNotThrow()
    {
        Span<char> buffer = Span<char>.Empty;
        RandomNumberGenerator.GetHexString(buffer);
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_FillsDestination()
    {
        ReadOnlySpan<int> choices = (ReadOnlySpan<int>)new int[] { 1, 2, 3, 4, 5 };
        Span<int> destination = new int[100];
        RandomNumberGenerator.GetItems(choices, destination);

        Assert.All(destination.ToArray(), value => Assert.InRange(value, 1, 5));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_EmptyDestination_DoesNotThrow()
    {
        ReadOnlySpan<int> choices = (ReadOnlySpan<int>)new int[] { 1, 2, 3 };
        Span<int> destination = Span<int>.Empty;
        RandomNumberGenerator.GetItems(choices, destination);
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_EmptyChoices_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => RandomNumberGenerator.GetItems(ReadOnlySpan<int>.Empty, new int[10].AsSpan()));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_ReturnsArray()
    {
        ReadOnlySpan<string> choices = (ReadOnlySpan<string>)new[] { "a", "b", "c", "d" };
        string[] result = RandomNumberGenerator.GetItems(choices, 50);

        Assert.Equal(50, result.Length);
        Assert.All(result, value => Assert.Contains(value, new[] { "a", "b", "c", "d" }));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_ZeroLength_ReturnsEmptyArray()
    {
        ReadOnlySpan<int> choices = (ReadOnlySpan<int>)new int[] { 1, 2, 3 };
        int[] result = RandomNumberGenerator.GetItems(choices, 0);
        Assert.Empty(result);
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        ReadOnlySpan<int> choices = (ReadOnlySpan<int>)new int[] { 1, 2, 3 };
        try
        {
            RandomNumberGenerator.GetItems(choices, -1);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Expected
        }
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_EmptyChoices_ThrowsArgumentException()
    {
        ReadOnlySpan<int> empty = ReadOnlySpan<int>.Empty;
        try
        {
            RandomNumberGenerator.GetItems<int>(empty, 10);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            // Expected
        }
    }

    [Fact]
    public void GetString_ReturnsStringOfCorrectLength()
    {
        ReadOnlySpan<char> choices = (ReadOnlySpan<char>)"abcdefghijklmnopqrstuvwxyz";
        string result = RandomNumberGenerator.GetString(choices, 50);

        Assert.Equal(50, result.Length);
        Assert.All(result, c => Assert.Contains(c, "abcdefghijklmnopqrstuvwxyz"));
    }

    [Fact]
    public void GetString_ZeroLength_ReturnsEmptyString()
    {
        ReadOnlySpan<char> choices = (ReadOnlySpan<char>)"abc";
        string result = RandomNumberGenerator.GetString(choices, 0);
        Assert.Empty(result);
    }

    [Fact]
    public void GetString_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        ReadOnlySpan<char> choices = (ReadOnlySpan<char>)"abc";
        try
        {
            RandomNumberGenerator.GetString(choices, -1);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Expected
        }
    }

    [Fact]
    public void GetString_EmptyChoices_ThrowsArgumentException()
    {
        ReadOnlySpan<char> empty = ReadOnlySpan<char>.Empty;
        try
        {
            RandomNumberGenerator.GetString(empty, 10);
            Assert.Fail("Expected ArgumentException");
        }
        catch (ArgumentException)
        {
            // Expected
        }
    }

    [Fact]
    public void Shuffle_ShufflesElements()
    {
        Span<int> values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var original = values.ToArray();

        RandomNumberGenerator.Shuffle(values);

        // Check all elements are still present
        Assert.Equal(original.OrderBy(x => x), values.ToArray().OrderBy(x => x));
        // Very unlikely to be in same order after shuffle (though possible)
        // We'll just verify the method executes without error
    }

    [Fact]
    public void Shuffle_EmptySpan_DoesNotThrow()
    {
        Span<int> values = Span<int>.Empty;
        RandomNumberGenerator.Shuffle(values);
    }

    [Fact]
    public void Shuffle_SingleElement_DoesNotThrow()
    {
        Span<int> values = new int[] { 42 };
        RandomNumberGenerator.Shuffle(values);
        Assert.Equal(42, values[0]);
    }

    [Fact]
    public void Shuffle_TwoElements_ShufflesOrKeepsSame()
    {
        Span<int> values = new int[] { 1, 2 };
        RandomNumberGenerator.Shuffle(values);
        Assert.Contains(1, values.ToArray());
        Assert.Contains(2, values.ToArray());
    }
}
