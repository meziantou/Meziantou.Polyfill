#pragma warning disable CA5394 // Do not use insecure randomness
using System;
using System.Linq;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class RandomTests
{
    [Fact]
    public void GetItems_ReadOnlySpan_Int_ReturnsArray()
    {
        var random = new Random(42);
        var choicesArray = new[] { 1, 2, 3, 4, 5 };

        var result = random.GetItems((ReadOnlySpan<int>)choicesArray, 10);

        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
        Assert.All(result, item => Assert.Contains(item, choicesArray));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_EmptyChoices_ThrowsArgumentException()
    {
        var random = new Random();
        var choicesArray = Array.Empty<int>();

        Assert.Throws<ArgumentException>(() => random.GetItems((ReadOnlySpan<int>)choicesArray, 5));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        var random = new Random();
        var choicesArray = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentOutOfRangeException>(() => random.GetItems((ReadOnlySpan<int>)choicesArray, -1));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Int_ZeroLength_ReturnsEmptyArray()
    {
        var random = new Random();
        var choicesArray = new[] { 1, 2, 3 };

        var result = random.GetItems((ReadOnlySpan<int>)choicesArray, 0);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_FillsDestination()
    {
        var random = new Random(42);
        var choicesArray = new[] { "a", "b", "c", "d" };
        var destination = new string[8];

        random.GetItems((ReadOnlySpan<string>)choicesArray, (Span<string>)destination);

        Assert.All(destination, item => Assert.Contains(item, choicesArray));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_EmptyChoices_ThrowsArgumentException()
    {
        var random = new Random();
        var choicesArray = Array.Empty<int>();
        var destination = new int[5];

        Assert.Throws<ArgumentException>(() => random.GetItems((ReadOnlySpan<int>)choicesArray, (Span<int>)destination));
    }

    [Fact]
    public void GetItems_ReadOnlySpan_Span_EmptyDestination_DoesNotThrow()
    {
        var random = new Random();
        var choicesArray = new[] { 1, 2, 3 };
        var destination = Array.Empty<int>();

        random.GetItems((ReadOnlySpan<int>)choicesArray, (Span<int>)destination);
    }

    [Fact]
    public void GetItems_Array_Int_ReturnsArray()
    {
        var random = new Random(42);
        var choices = new[] { 10, 20, 30, 40, 50 };

        var result = random.GetItems(choices, 15);

        Assert.NotNull(result);
        Assert.Equal(15, result.Length);
        Assert.All(result, item => Assert.Contains(item, choices));
    }

    [Fact]
    public void GetItems_Array_Int_NullChoices_ThrowsArgumentNullException()
    {
        var random = new Random();
        int[] choices = null!;

        Assert.Throws<ArgumentNullException>(() => random.GetItems(choices, 5));
    }

    [Fact]
    public void GetItems_Array_Int_EmptyChoices_ThrowsArgumentException()
    {
        var random = new Random();
        var choices = Array.Empty<int>();

        Assert.Throws<ArgumentException>(() => random.GetItems(choices, 5));
    }

    [Fact]
    public void GetItems_Array_Int_NegativeLength_ThrowsArgumentOutOfRangeException()
    {
        var random = new Random();
        var choices = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentOutOfRangeException>(() => random.GetItems(choices, -1));
    }

    [Fact]
    public void NextBytes_Span_FillsBuffer()
    {
        var random = new Random(42);
        var buffer = new byte[20];

        random.NextBytes((Span<byte>)buffer);

        Assert.Contains(buffer, b => b != 0);
    }

    [Fact]
    public void NextBytes_Span_EmptyBuffer_DoesNotThrow()
    {
        var random = new Random();
        var buffer = Array.Empty<byte>();

        random.NextBytes((Span<byte>)buffer);
    }

    [Fact]
    public void NextBytes_Span_DifferentCallsProduceDifferentResults()
    {
        var random = new Random(42);
        var buffer1 = new byte[10];
        var buffer2 = new byte[10];

        random.NextBytes((Span<byte>)buffer1);
        random.NextBytes((Span<byte>)buffer2);

        Assert.NotEqual(buffer1, buffer2);
    }

    [Fact]
    public void Shuffle_Span_ShufflesElements()
    {
        var random = new Random(42);
        var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var original = (int[])values.Clone();

        random.Shuffle((Span<int>)values);

        Assert.Equal(original.Length, values.Length);
        Assert.All(original, item => Assert.Contains(item, values));
        Assert.NotEqual(original, values);
    }

    [Fact]
    public void Shuffle_Span_EmptySpan_DoesNotThrow()
    {
        var random = new Random();
        var values = Array.Empty<int>();

        random.Shuffle((Span<int>)values);
    }

    [Fact]
    public void Shuffle_Span_SingleElement_DoesNotThrow()
    {
        var random = new Random();
        var values = new[] { 42 };

        random.Shuffle((Span<int>)values);

        Assert.Equal(42, values[0]);
    }

    [Fact]
    public void Shuffle_Array_ShufflesElements()
    {
        var random = new Random(42);
        var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var original = (int[])values.Clone();

        random.Shuffle(values);

        Assert.Equal(original.Length, values.Length);
        Assert.All(original, item => Assert.Contains(item, values));
        Assert.NotEqual(original, values);
    }

    [Fact]
    public void Shuffle_Array_NullArray_ThrowsArgumentNullException()
    {
        var random = new Random();
        int[] values = null!;

        Assert.Throws<ArgumentNullException>(() => random.Shuffle(values));
    }

    [Fact]
    public void Shuffle_Array_EmptyArray_DoesNotThrow()
    {
        var random = new Random();
        var values = Array.Empty<int>();

        random.Shuffle(values);
    }

    [Fact]
    public void Shared_ReturnsNonNullInstance()
    {
        var shared = Random.Shared;

        Assert.NotNull(shared);
    }

    [Fact]
    public void Shared_ReturnsSameInstancePerThread()
    {
        var shared1 = Random.Shared;
        var shared2 = Random.Shared;

        Assert.Same(shared1, shared2);
    }

    [Fact]
    public void Shared_CanGenerateRandomNumbers()
    {
        var shared = Random.Shared;

        var value = shared.Next();

        Assert.InRange(value, 0, int.MaxValue);
    }

    [Fact]
    public void Shared_WorksWithAllMethods()
    {
        var shared = Random.Shared;

        // Test Next
        var nextValue = shared.Next(100);
        Assert.InRange(nextValue, 0, 100);

        // Test NextBytes
        var bytes = new byte[10];
        shared.NextBytes(bytes);
        Assert.Contains(bytes, b => b != 0);

        // Test NextDouble
        var doubleValue = shared.NextDouble();
        Assert.InRange(doubleValue, 0.0, 1.0);
    }

    [Fact]
    public void GetItems_WithSingleChoice_ReturnsOnlyThatChoice()
    {
        var random = new Random();
        var choicesArray = new[] { 42 };

        var result = random.GetItems((ReadOnlySpan<int>)choicesArray, 10);

        Assert.All(result, item => Assert.Equal(42, item));
    }

    [Fact]
    public void Shuffle_MaintainsAllElements()
    {
        var random = new Random(42);
        var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var original = (int[])values.Clone();

        random.Shuffle(values);

        Array.Sort(values);
        Array.Sort(original);
        Assert.Equal(original, values);
    }
}
