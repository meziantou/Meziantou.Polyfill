using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

partial class PolyfillExtensions
{
    extension(TextWriter)
    {
        public static TextWriter CreateBroadcasting(params TextWriter[] writers)
        {
            if (writers is null)
                throw new ArgumentNullException(nameof(writers));

            if (writers.Length == 0)
                return TextWriter.Null;

            var copy = new TextWriter[writers.Length];
            Array.Copy(writers, copy, writers.Length);
            return new BroadcastingTextWriter(copy);
        }
    }
}

file sealed class BroadcastingTextWriter : TextWriter
{
    private readonly TextWriter[] _writers;

    public BroadcastingTextWriter(TextWriter[] writers)
    {
        for (var i = 0; i < writers.Length; i++)
        {
            if (writers[i] is null)
                throw new ArgumentNullException(nameof(writers));
        }

        _writers = writers;
    }

    public override Encoding Encoding => _writers[0].Encoding;

    public override IFormatProvider FormatProvider => _writers[0].FormatProvider;

    public override string NewLine
    {
        get => base.NewLine;
        set
        {
            base.NewLine = value;
            for (var i = 0; i < _writers.Length; i++)
            {
                _writers[i].NewLine = value;
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposing)
            return;

        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Dispose();
        }
    }

    public override void Flush()
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Flush();
        }
    }

    public override async Task FlushAsync()
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].FlushAsync().ConfigureAwait(false);
        }
    }

    public override void Write(bool value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(char value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(char[]? buffer)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(buffer);
        }
    }

    public override void Write(char[] buffer, int index, int count)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(buffer, index, count);
        }
    }

    public override void Write(decimal value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(double value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(float value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(int value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(long value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(object? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(string? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(uint value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(ulong value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(value);
        }
    }

    public override void Write(string format, object? arg0)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(format, arg0);
        }
    }

    public override void Write(string format, object? arg0, object? arg1)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(format, arg0, arg1);
        }
    }

    public override void Write(string format, object? arg0, object? arg1, object? arg2)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(format, arg0, arg1, arg2);
        }
    }

    public override void Write(string format, params object?[] arg)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].Write(format, arg);
        }
    }

    public override void WriteLine()
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine();
        }
    }

    public override void WriteLine(bool value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(char value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(char[]? buffer)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(buffer);
        }
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(buffer, index, count);
        }
    }

    public override void WriteLine(decimal value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(double value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(float value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(int value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(long value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(object? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(string? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(uint value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(ulong value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(value);
        }
    }

    public override void WriteLine(string format, object? arg0)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(format, arg0);
        }
    }

    public override void WriteLine(string format, object? arg0, object? arg1)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(format, arg0, arg1);
        }
    }

    public override void WriteLine(string format, object? arg0, object? arg1, object? arg2)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(format, arg0, arg1, arg2);
        }
    }

    public override void WriteLine(string format, params object?[] arg)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            _writers[i].WriteLine(format, arg);
        }
    }

    public override async Task WriteAsync(char value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteAsync(value).ConfigureAwait(false);
        }
    }

    public override async Task WriteAsync(string? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteAsync(value).ConfigureAwait(false);
        }
    }

    public override async Task WriteAsync(char[] buffer, int index, int count)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteAsync(buffer, index, count).ConfigureAwait(false);
        }
    }

    public override async Task WriteLineAsync()
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteLineAsync().ConfigureAwait(false);
        }
    }

    public override async Task WriteLineAsync(char value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteLineAsync(value).ConfigureAwait(false);
        }
    }

    public override async Task WriteLineAsync(string? value)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteLineAsync(value).ConfigureAwait(false);
        }
    }

    public override async Task WriteLineAsync(char[] buffer, int index, int count)
    {
        for (var i = 0; i < _writers.Length; i++)
        {
            await _writers[i].WriteLineAsync(buffer, index, count).ConfigureAwait(false);
        }
    }
}
