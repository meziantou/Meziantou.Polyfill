using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemBuffersTests
{
    [Fact]
    public void System_Buffers_SearchValues_Create_Byte()
    {
        ReadOnlySpan<byte> span = new byte[] { 1, 2, 3, 4, 5 };
        var values = System.Buffers.SearchValues.Create(span);
        Assert.NotNull(values);
    }

    [Fact]
    public void System_Buffers_SearchValues_Create_Char()
    {
        ReadOnlySpan<char> span = "abcde".AsSpan();
        var values = System.Buffers.SearchValues.Create(span);
        Assert.NotNull(values);
    }

#if NET9_0_OR_GREATER
    [Fact]
    public void System_Buffers_SearchValues_Create_String_Ordinal()
    {
        ReadOnlySpan<string> span = new[] { "hello", "world", "test" };
        var values = System.Buffers.SearchValues.Create(span, StringComparison.Ordinal);
        Assert.NotNull(values);
    }

    [Fact]
    public void System_Buffers_SearchValues_Create_String_OrdinalIgnoreCase()
    {
        ReadOnlySpan<string> span = new[] { "hello", "world", "test" };
        var values = System.Buffers.SearchValues.Create(span, StringComparison.OrdinalIgnoreCase);
        Assert.NotNull(values);
    }
#endif

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public void SequenceReader_Constructor_EmptySequence()
    {
        var sequence = new System.Buffers.ReadOnlySequence<byte>();
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(0, reader.Consumed);
        Assert.Equal(0, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_Constructor_SingleSegment()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(0, reader.Consumed);
        Assert.Equal(5, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_Advance_Valid()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(2);
        Assert.Equal(2, reader.Consumed);
        Assert.Equal(3, reader.Remaining);
        
        reader.Advance(3);
        Assert.Equal(5, reader.Consumed);
        Assert.Equal(0, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_Advance_Zero()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(0);
        Assert.Equal(0, reader.Consumed);
        Assert.Equal(3, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_Advance_Negative_ThrowsException()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        try
        {
            reader.Advance(-1);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Expected
        }
    }

    [Fact]
    public void SequenceReader_Advance_TooMuch_ThrowsException()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        try
        {
            reader.Advance(4);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }
        catch (ArgumentOutOfRangeException)
        {
            // Expected
        }
    }

    [Fact]
    public void SequenceReader_TryCopyTo_Success()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Span<byte> destination = new byte[3];
        Assert.True(reader.TryCopyTo(destination));
        Assert.Equal(new byte[] { 1, 2, 3 }, destination.ToArray());
        
        // Consumed should not change after TryCopyTo
        Assert.Equal(0, reader.Consumed);
    }

    [Fact]
    public void SequenceReader_TryCopyTo_InsufficientData()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Span<byte> destination = new byte[5];
        Assert.False(reader.TryCopyTo(destination));
    }

    [Fact]
    public void SequenceReader_TryCopyTo_EmptyDestination()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Span<byte> destination = Span<byte>.Empty;
        Assert.True(reader.TryCopyTo(destination));
    }

    [Fact]
    public void SequenceReader_TryCopyTo_AfterAdvance()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(2);
        
        Span<byte> destination = new byte[2];
        Assert.True(reader.TryCopyTo(destination));
        Assert.Equal(new byte[] { 3, 4 }, destination.ToArray());
    }

    [Fact]
    public void SequenceReader_Position()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        var position1 = reader.Position;
        reader.Advance(2);
        var position2 = reader.Position;
        
        Assert.NotEqual(position1, position2);
        Assert.Equal(2, sequence.Slice(position1, position2).Length);
    }
#endif

}
