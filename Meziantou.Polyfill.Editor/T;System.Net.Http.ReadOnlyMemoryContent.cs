using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
    internal sealed class ReadOnlyMemoryContent : HttpContent
    {
        private readonly ReadOnlyMemory<byte> _content;

        public ReadOnlyMemoryContent(ReadOnlyMemory<byte> content) =>
            _content = content;

#if NET5_0_OR_GREATER
        protected override void SerializeToStream(Stream stream, TransportContext? context, CancellationToken cancellationToken)
        {
            stream.Write(_content.Span);
        }
#endif

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) =>
            stream.WriteAsync(_content).AsTask();

#if NET5_0_OR_GREATER
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context, CancellationToken cancellationToken) =>
            stream.WriteAsync(_content, cancellationToken).AsTask();
#endif

        protected override bool TryComputeLength(out long length)
        {
            length = _content.Length;
            return true;
        }

#if NET5_0_OR_GREATER
        protected override Stream CreateContentReadStream(CancellationToken cancellationToken) =>
            new ReadOnlyMemoryStream(_content);
#endif

        protected override Task<Stream> CreateContentReadStreamAsync() =>
            Task.FromResult<Stream>(new ReadOnlyMemoryStream(_content));
    }
}

file sealed class ReadOnlyMemoryStream : Stream
{
    private ReadOnlyMemory<byte> _content;
    private int _position;
    private bool _isOpen;

    public ReadOnlyMemoryStream(ReadOnlyMemory<byte> content)
    {
        _content = content;
        _isOpen = true;
    }

    public override bool CanRead => _isOpen;
    public override bool CanSeek => _isOpen;
    public override bool CanWrite => false;

    private void EnsureNotClosed()
    {
        if (!_isOpen)
        {
            throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
        }
    }

    public override long Length
    {
        get
        {
            EnsureNotClosed();
            return _content.Length;
        }
    }

    public override long Position
    {
        get
        {
            EnsureNotClosed();
            return _position;
        }
        set
        {
            EnsureNotClosed();
            if (value < 0 || value > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _position = (int)value;
        }
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        EnsureNotClosed();

        long pos =
            origin == SeekOrigin.Begin ? offset :
            origin == SeekOrigin.Current ? _position + offset :
            origin == SeekOrigin.End ? _content.Length + offset :
            throw new ArgumentOutOfRangeException(nameof(origin));

        if (pos > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }
        else if (pos < 0)
        {
            throw new IOException("An attempt was made to move the position before the beginning of the stream.");
        }

        _position = (int)pos;
        return _position;
    }

    public override int ReadByte()
    {
        EnsureNotClosed();

        ReadOnlySpan<byte> s = _content.Span;
        return _position < s.Length ? s[_position++] : -1;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        ValidateBufferArguments(buffer, offset, count);
        return ReadBuffer(new Span<byte>(buffer, offset, count));
    }

#if !NETFRAMEWORK && !NETSTANDARD2_0
    public override int Read(Span<byte> buffer) => ReadBuffer(buffer);
#endif

    private int ReadBuffer(Span<byte> buffer)
    {
        EnsureNotClosed();

        int remaining = _content.Length - _position;

        if (remaining <= 0 || buffer.Length == 0)
        {
            return 0;
        }
        else if (remaining <= buffer.Length)
        {
            _content.Span.Slice(_position).CopyTo(buffer);
            _position = _content.Length;
            return remaining;
        }
        else
        {
            _content.Span.Slice(_position, buffer.Length).CopyTo(buffer);
            _position += buffer.Length;
            return buffer.Length;
        }
    }

    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        ValidateBufferArguments(buffer, offset, count);
        EnsureNotClosed();
        return cancellationToken.IsCancellationRequested ?
            Task.FromCanceled<int>(cancellationToken) :
            Task.FromResult(ReadBuffer(new Span<byte>(buffer, offset, count)));
    }

#if !NETFRAMEWORK && !NETSTANDARD2_0
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
    {
        EnsureNotClosed();
        return cancellationToken.IsCancellationRequested ?
            ValueTask.FromCanceled<int>(cancellationToken) :
            new ValueTask<int>(ReadBuffer(buffer.Span));
    }
#endif

    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state) =>
        TaskToAsyncResult.Begin(ReadAsync(buffer, offset, count), callback, state);

    public override int EndRead(IAsyncResult asyncResult)
    {
        EnsureNotClosed();
        return TaskToAsyncResult.End<int>(asyncResult);
    }

#if !NETFRAMEWORK && !NETSTANDARD2_0
    public override void CopyTo(Stream destination, int bufferSize)
    {
        ValidateCopyToArguments(destination, bufferSize);
        EnsureNotClosed();
        if (_content.Length > _position)
        {
            destination.Write(_content.Span.Slice(_position));
            _position = _content.Length;
        }
    }

    public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
    {
        ValidateCopyToArguments(destination, bufferSize);
        EnsureNotClosed();
        if (_content.Length > _position)
        {
            ReadOnlyMemory<byte> content = _content.Slice(_position);
            _position = _content.Length;
            return destination.WriteAsync(content, cancellationToken).AsTask();
        }
        else
        {
            return Task.CompletedTask;
        }
    }
#endif

    public override void Flush() { }

    public override Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        _isOpen = false;
        _content = default;
        base.Dispose(disposing);
    }

#if NETFRAMEWORK || NETSTANDARD2_0
        private static void ValidateBufferArguments(byte[] buffer, int offset, int count)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
 
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");
            }
 
            if ((uint)count > buffer.Length - offset)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
        }
#endif
}