using System;
using System.Buffers.Binary;
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
    public void BinaryPrimitives_ReadFloatingPointValues()
    {
        Assert.Equal(1d, BinaryPrimitives.ReadDoubleBigEndian([0x3f, 0xf0, 0, 0, 0, 0, 0, 0]));
        Assert.Equal(1d, BinaryPrimitives.ReadDoubleLittleEndian([0, 0, 0, 0, 0, 0, 0xf0, 0x3f]));
        Assert.Equal(1f, BinaryPrimitives.ReadSingleBigEndian([0x3f, 0x80, 0, 0]));
        Assert.Equal(1f, BinaryPrimitives.ReadSingleLittleEndian([0, 0, 0x80, 0x3f]));

        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.ReadDoubleBigEndian(new byte[sizeof(double) - 1]));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.ReadDoubleLittleEndian(new byte[sizeof(double) - 1]));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.ReadSingleBigEndian(new byte[sizeof(float) - 1]));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.ReadSingleLittleEndian(new byte[sizeof(float) - 1]));
    }

    [Fact]
    public void BinaryPrimitives_TryReadFloatingPointValues()
    {
        Assert.True(BinaryPrimitives.TryReadDoubleBigEndian([0x3f, 0xf0, 0, 0, 0, 0, 0, 0], out var doubleValue));
        Assert.Equal(1d, doubleValue);
        Assert.True(BinaryPrimitives.TryReadDoubleLittleEndian([0, 0, 0, 0, 0, 0, 0xf0, 0x3f], out doubleValue));
        Assert.Equal(1d, doubleValue);
        Assert.True(BinaryPrimitives.TryReadSingleBigEndian([0x3f, 0x80, 0, 0], out var singleValue));
        Assert.Equal(1f, singleValue);
        Assert.True(BinaryPrimitives.TryReadSingleLittleEndian([0, 0, 0x80, 0x3f], out singleValue));
        Assert.Equal(1f, singleValue);

        Assert.False(BinaryPrimitives.TryReadDoubleBigEndian(new byte[sizeof(double) - 1], out doubleValue));
        Assert.Equal(default, doubleValue);
        Assert.False(BinaryPrimitives.TryReadDoubleLittleEndian(new byte[sizeof(double) - 1], out doubleValue));
        Assert.Equal(default, doubleValue);
        Assert.False(BinaryPrimitives.TryReadSingleBigEndian(new byte[sizeof(float) - 1], out singleValue));
        Assert.Equal(default, singleValue);
        Assert.False(BinaryPrimitives.TryReadSingleLittleEndian(new byte[sizeof(float) - 1], out singleValue));
        Assert.Equal(default, singleValue);
    }

    [Fact]
    public void BinaryPrimitives_WriteFloatingPointValues()
    {
        var doubleDestination = new byte[sizeof(double)];
        BinaryPrimitives.WriteDoubleBigEndian(doubleDestination, 1d);
        Assert.Equal([0x3f, 0xf0, 0, 0, 0, 0, 0, 0], doubleDestination);
        BinaryPrimitives.WriteDoubleLittleEndian(doubleDestination, 1d);
        Assert.Equal([0, 0, 0, 0, 0, 0, 0xf0, 0x3f], doubleDestination);

        var singleDestination = new byte[sizeof(float)];
        BinaryPrimitives.WriteSingleBigEndian(singleDestination, 1f);
        Assert.Equal([0x3f, 0x80, 0, 0], singleDestination);
        BinaryPrimitives.WriteSingleLittleEndian(singleDestination, 1f);
        Assert.Equal([0, 0, 0x80, 0x3f], singleDestination);

        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.WriteDoubleBigEndian(new byte[sizeof(double) - 1], 1d));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.WriteDoubleLittleEndian(new byte[sizeof(double) - 1], 1d));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.WriteSingleBigEndian(new byte[sizeof(float) - 1], 1f));
        Assert.Throws<ArgumentOutOfRangeException>(() => BinaryPrimitives.WriteSingleLittleEndian(new byte[sizeof(float) - 1], 1f));
    }

    [Fact]
    public void BinaryPrimitives_TryWriteFloatingPointValues()
    {
        var doubleDestination = new byte[sizeof(double)];
        Assert.True(BinaryPrimitives.TryWriteDoubleBigEndian(doubleDestination, 1d));
        Assert.Equal([0x3f, 0xf0, 0, 0, 0, 0, 0, 0], doubleDestination);
        Assert.True(BinaryPrimitives.TryWriteDoubleLittleEndian(doubleDestination, 1d));
        Assert.Equal([0, 0, 0, 0, 0, 0, 0xf0, 0x3f], doubleDestination);

        var singleDestination = new byte[sizeof(float)];
        Assert.True(BinaryPrimitives.TryWriteSingleBigEndian(singleDestination, 1f));
        Assert.Equal([0x3f, 0x80, 0, 0], singleDestination);
        Assert.True(BinaryPrimitives.TryWriteSingleLittleEndian(singleDestination, 1f));
        Assert.Equal([0, 0, 0x80, 0x3f], singleDestination);

        Assert.False(BinaryPrimitives.TryWriteDoubleBigEndian(new byte[sizeof(double) - 1], 1d));
        Assert.False(BinaryPrimitives.TryWriteDoubleLittleEndian(new byte[sizeof(double) - 1], 1d));
        Assert.False(BinaryPrimitives.TryWriteSingleBigEndian(new byte[sizeof(float) - 1], 1f));
        Assert.False(BinaryPrimitives.TryWriteSingleLittleEndian(new byte[sizeof(float) - 1], 1f));
    }

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

    [Fact]
    public void SequenceReader_Length()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(5, reader.Length);
    }

    [Fact]
    public void SequenceReader_Sequence()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(sequence.Start, reader.Sequence.Start);
        Assert.Equal(sequence.End, reader.Sequence.End);
    }

    [Fact]
    public void SequenceReader_End()
    {
        var data = new byte[] { 1, 2, 3 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.False(reader.End);
        reader.Advance(3);
        Assert.True(reader.End);
    }

    [Fact]
    public void SequenceReader_CurrentSpan()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(5, reader.CurrentSpan.Length);
        Assert.Equal(1, reader.CurrentSpan[0]);
    }

    [Fact]
    public void SequenceReader_CurrentSpanIndex()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.Equal(0, reader.CurrentSpanIndex);
    }

    [Fact]
    public void SequenceReader_UnreadSequence()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(2);
        var unread = reader.UnreadSequence;
        Assert.Equal(3, unread.Length);
    }

    [Fact]
    public void SequenceReader_UnreadSpan()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        var unreadSpan = reader.UnreadSpan;
        Assert.Equal(5, unreadSpan.Length);
    }

    [Fact]
    public void SequenceReader_AdvanceLong()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(2L);
        Assert.Equal(2, reader.Consumed);
        Assert.Equal(3, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_Rewind()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        reader.Advance(3);
        Assert.Equal(3, reader.Consumed);
        
        reader.Rewind(2);
        Assert.Equal(1, reader.Consumed);
        Assert.Equal(4, reader.Remaining);
    }

    [Fact]
    public void SequenceReader_TryPeek()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.True(reader.TryPeek(out var value));
        Assert.Equal(1, value);
        Assert.Equal(0, reader.Consumed); // TryPeek should not advance
    }

    [Fact]
    public void SequenceReader_TryPeekWithOffset()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.True(reader.TryPeek(2, out var value));
        Assert.Equal(3, value);
        Assert.Equal(0, reader.Consumed); // TryPeek should not advance
    }

    [Fact]
    public void SequenceReader_TryRead()
    {
        var data = new byte[] { 1, 2, 3, 4, 5 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.True(reader.TryRead(out var value1));
        Assert.Equal(1, value1);
        Assert.Equal(1, reader.Consumed);
        
        Assert.True(reader.TryRead(out var value2));
        Assert.Equal(2, value2);
        Assert.Equal(2, reader.Consumed);
    }

    [Fact]
    public void SequenceReader_TryReadAtEnd()
    {
        var data = new byte[] { 1 };
        var sequence = new System.Buffers.ReadOnlySequence<byte>(data);
        var reader = new System.Buffers.SequenceReader<byte>(sequence);
        
        Assert.True(reader.TryRead(out var value));
        Assert.Equal(1, value);
        
        Assert.False(reader.TryRead(out _));
    }

}
