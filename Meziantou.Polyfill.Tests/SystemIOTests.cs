using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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

public class SystemIOTests
{
    [Fact]
    public void TextWriter_CreateBroadcasting_WritesToAllWriters()
    {
        using var first = new StringWriter();
        using var second = new StringWriter();
        using var writer = TextWriter.CreateBroadcasting(first, second);

        writer.Write("test");
        writer.WriteLine('!');

        Assert.Equal(first.ToString(), second.ToString());
        Assert.Equal("test!" + writer.NewLine, first.ToString());
    }

    [Fact]
    public void TextWriter_CreateBroadcasting_Empty_ReturnsNullWriter()
    {
        var writer = TextWriter.CreateBroadcasting();
        Assert.Same(TextWriter.Null, writer);
    }

    [Fact]
    public void TextWriter_CreateBroadcasting_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => TextWriter.CreateBroadcasting(null!));
    }

    [Fact]
    public void TextWriter_CreateBroadcasting_NullWriter_ThrowsArgumentNullException()
    {
        var first = new StringWriter();
        Assert.Throws<ArgumentNullException>(() => TextWriter.CreateBroadcasting(first, null!));
    }

    [Fact]
    public void TextWriter_CreateBroadcasting_ForwardsEncodingAndFormatProviderFromFirstWriter()
    {
        var firstProvider = CultureInfo.GetCultureInfo("fr-FR");
        using var first = new MetadataTextWriter(Encoding.UTF32, firstProvider);
        using var second = new MetadataTextWriter(Encoding.ASCII, CultureInfo.InvariantCulture);
        using var writer = TextWriter.CreateBroadcasting(first, second);

        Assert.Same(first.Encoding, writer.Encoding);
        Assert.Same(first.FormatProvider, writer.FormatProvider);
    }

    [Fact]
    public void TextWriter_CreateBroadcasting_WriterExceptionStopsFollowingWriters()
    {
        using var first = new StringWriter();
        using var second = new ThrowingTextWriter();
        using var third = new StringWriter();
        using var writer = TextWriter.CreateBroadcasting(first, second, third);

        Assert.Throws<InvalidOperationException>(() => writer.Write('x'));
        Assert.Equal("x", first.ToString());
        Assert.Empty(third.ToString());
    }

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public async Task StreamWriter_WriteAsync()
    {
        using var sr = new System.IO.StringWriter();
        await sr.WriteAsync("test".AsMemory(), CancellationToken.None);
        Assert.Equal("test", sr.ToString());
    }
#endif

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public void StreamWriter_Write()
    {
        using var ms = new MemoryStream();
        ms.Write([1, 2]);
        Assert.Equal([1, 2], ms.ToArray());
    }
#endif

    [Fact]
    public async Task StreamReader_ReadToEndAsync()
    {
        using var sr = new StringReader("test");
        var result = await sr.ReadToEndAsync(CancellationToken.None);
        Assert.Equal("test", result);
    }

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public async Task StreamReader_ReadAsync()
    {
        using var sr = new StringReader("test");
        var buffer = new char[2];
        var result = await sr.ReadAsync(buffer.AsMemory(), CancellationToken.None);
        Assert.Equal(2, result);
        Assert.Equal("te", new string(buffer));
    }

    [Fact]
    public async Task StreamReader_ReadAsync2()
    {
        using var sr = new MemoryStream([3, 4, 5]);
        var buffer = new byte[3];
        buffer[0] = 1;
        var result = await sr.ReadAsync(buffer.AsMemory()[1..], CancellationToken.None);
        Assert.Equal(2, result);
        Assert.Equal([1, 3, 4], buffer);
    }

    [Fact]
    public async Task StreamReader_ReadLineAsync()
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes("ab\ncd"));
        using var reader = new StreamReader(ms, Encoding.UTF8);

        Assert.Equal("ab", await reader.ReadLineAsync());
        Assert.Equal("cd", await reader.ReadLineAsync(CancellationToken.None));
        Assert.Null(await reader.ReadLineAsync(CancellationToken.None));
    }

    [Fact]
    public void Stream_ReadAtLeast()
    {
        using var ms = new MemoryStream([1, 2, 3, 4]);

        var buffer = new byte[4];
        Assert.Equal(4, ms.ReadAtLeast(buffer.AsSpan(), 4));
        Assert.Equal([1, 2, 3, 4], buffer);
    }

    [Fact]
    public async Task Stream_ReadAtLeastAsync()
    {
        using var ms = new MemoryStream([1, 2, 3, 4]);

        var buffer = new byte[4];
        Assert.Equal(4, await ms.ReadAtLeastAsync(buffer.AsMemory(), 4));
        Assert.Equal([1, 2, 3, 4], buffer);
    }

    [Fact]
    public async Task File_WriteAllTextAsync_ReadOnlyMemory()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            // Write with default encoding (UTF8 without BOM)
            ReadOnlyMemory<char> content = "Hello, World!".AsMemory();
            await File.WriteAllTextAsync(tempFile, content);

            var result = File.ReadAllText(tempFile);
            Assert.Equal("Hello, World!", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task Stream_ReadAsync_Memory()
    {
        using var ms = new MemoryStream([1, 2, 3, 4, 5]);
        var buffer = new byte[3];

        var bytesRead = await ms.ReadAsync(buffer.AsMemory());

        Assert.Equal(3, bytesRead);
        Assert.Equal([1, 2, 3], buffer);
    }

    [Fact]
    public async Task Stream_WriteAsync_ReadOnlyMemory()
    {
        using var ms = new MemoryStream();
        var data = new byte[] { 1, 2, 3, 4, 5 };

        await ms.WriteAsync(data.AsMemory());

        Assert.Equal([1, 2, 3, 4, 5], ms.ToArray());
    }

    [Fact]
    public async Task Stream_DisposeAsync_IAsyncDisposable()
    {
        var stream = new AsyncDisposableStream();

        await stream.DisposeAsync();

        Assert.True(stream.AsyncDisposed);
        Assert.False(stream.SyncDisposed);
    }

    [Fact]
    public async Task Stream_DisposeAsync_NotAsyncDisposable()
    {
        var stream = new SyncOnlyDisposableStream();

        await stream.DisposeAsync();

        Assert.True(stream.Disposed);
    }

    private sealed class AsyncDisposableStream : MemoryStream, IAsyncDisposable
    {
        public bool AsyncDisposed { get; private set; }
        public bool SyncDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            SyncDisposed = true;
            base.Dispose(disposing);
        }

#if NET8_0_OR_GREATER
#pragma warning disable CA2215 // Dispose methods should call base class dispose
        public override ValueTask DisposeAsync()
#pragma warning restore CA2215
#else
        public ValueTask DisposeAsync()
#endif
        {
            AsyncDisposed = true;
            return default;
        }
    }

    private sealed class SyncOnlyDisposableStream : MemoryStream
    {
        public bool Disposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            Disposed = true;
            base.Dispose(disposing);
        }
    }
#endif

    [Fact]
    public void Path_GetRelativePath_SameDirectory()
    {
        var from = Path.GetTempPath();
        var to = Path.GetTempPath();
        var result = Path.GetRelativePath(from, to);
        Assert.Equal(".", result);
    }

    [Fact]
    public void Path_GetRelativePath_SubDirectory()
    {
        var from = Path.GetTempPath();
        var to = Path.Combine(Path.GetTempPath(), "subfolder");
        var result = Path.GetRelativePath(from, to);
        Assert.Equal("subfolder", result);
    }

    [Fact]
    public void Path_GetRelativePath_ParentDirectory()
    {
        var from = Path.Combine(Path.GetTempPath(), "subfolder");
        var to = Path.GetTempPath();
        var result = Path.GetRelativePath(from, to);
        Assert.Equal("..", result);
    }

    [Fact]
    public void Path_GetRelativePath_SiblingDirectory()
    {
        var from = Path.Combine(Path.GetTempPath(), "a");
        var to = Path.Combine(Path.GetTempPath(), "b");
        var result = Path.GetRelativePath(from, to);
        Assert.Equal(Path.Combine("..", "b"), result);
    }

    [Fact]
    public void Path_GetRelativePath_NullRelativeTo_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Path.GetRelativePath(null!, "path"));
    }

    [Fact]
    public void Path_GetRelativePath_NullPath_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Path.GetRelativePath("relativeTo", null!));
    }

    [Fact]
    public void Path_GetRelativePath_SameDirectory_WithTrailingSeparator()
    {
        // Path.GetTempPath() returns a trailing separator on some platforms; polyfill must handle it
        var sep = Path.DirectorySeparatorChar.ToString();
        var from = Path.Combine(Path.GetTempPath(), "testfolder") + sep;
        var to = Path.Combine(Path.GetTempPath(), "testfolder") + sep;
        var result = Path.GetRelativePath(from, to);
        Assert.Equal(".", result);
    }

    [Fact]
    public void Path_GetRelativePath_SubDirectory_WithTrailingSeparatorOnBase()
    {
        var sep = Path.DirectorySeparatorChar.ToString();
        var from = Path.Combine(Path.GetTempPath(), "testfolder") + sep;
        var to = Path.Combine(Path.GetTempPath(), "testfolder", "child");
        var result = Path.GetRelativePath(from, to);
        Assert.Equal("child", result);
    }

    private sealed class MetadataTextWriter : StringWriter
    {
        private readonly Encoding _encoding;

        public MetadataTextWriter(Encoding encoding, IFormatProvider formatProvider)
            : base(formatProvider) =>
            _encoding = encoding;

        public override Encoding Encoding => _encoding;
    }

    private sealed class ThrowingTextWriter : StringWriter
    {
        public override void Write(char value) => throw new InvalidOperationException("boom");
    }

}
