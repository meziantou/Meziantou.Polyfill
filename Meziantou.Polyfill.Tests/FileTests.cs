using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class FileTests
{
    [Fact]
    public void AppendAllBytes_ByteArray()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(tempFile, new byte[] { 1, 2, 3 });
            File.AppendAllBytes(tempFile, new byte[] { 4, 5, 6 });
            var result = File.ReadAllBytes(tempFile);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6 }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void AppendAllBytes_ReadOnlySpan()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(tempFile, new byte[] { 1, 2, 3 });
            ReadOnlySpan<byte> data = new byte[] { 4, 5, 6 };
            File.AppendAllBytes(tempFile, data);
            var result = File.ReadAllBytes(tempFile);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6 }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllBytesAsync_ByteArray()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, new byte[] { 1, 2, 3 });
            await File.AppendAllBytesAsync(tempFile, new byte[] { 4, 5, 6 });
            var result = await File.ReadAllBytesAsync(tempFile);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6 }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllBytesAsync_ReadOnlyMemory()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(tempFile, new byte[] { 1, 2, 3 });
            ReadOnlyMemory<byte> data = new byte[] { 4, 5, 6 };
            await File.AppendAllBytesAsync(tempFile, data);
            var result = await File.ReadAllBytesAsync(tempFile);
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6 }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllLinesAsync_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2" });
            await File.AppendAllLinesAsync(tempFile, new[] { "line3", "line4" });
            var result = await File.ReadAllLinesAsync(tempFile);
            Assert.Equal(new[] { "line1", "line2", "line3", "line4" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllLinesAsync_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2" }, Encoding.UTF8);
            await File.AppendAllLinesAsync(tempFile, new[] { "line3", "line4" }, Encoding.UTF8);
            var result = await File.ReadAllLinesAsync(tempFile, Encoding.UTF8);
            Assert.Equal(new[] { "line1", "line2", "line3", "line4" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void AppendAllText_ReadOnlySpan_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, "Hello");
            ReadOnlySpan<char> data = " World".AsSpan();
            File.AppendAllText(tempFile, data);
            var result = File.ReadAllText(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void AppendAllText_ReadOnlySpan_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, "Hello", Encoding.UTF8);
            ReadOnlySpan<char> data = " World".AsSpan();
            File.AppendAllText(tempFile, data, Encoding.UTF8);
            var result = File.ReadAllText(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllTextAsync_ReadOnlyMemory_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello");
            ReadOnlyMemory<char> data = " World".AsMemory();
            await File.AppendAllTextAsync(tempFile, data);
            var result = await File.ReadAllTextAsync(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllTextAsync_ReadOnlyMemory_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello", Encoding.UTF8);
            ReadOnlyMemory<char> data = " World".AsMemory();
            await File.AppendAllTextAsync(tempFile, data, Encoding.UTF8);
            var result = await File.ReadAllTextAsync(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllTextAsync_String_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello");
            await File.AppendAllTextAsync(tempFile, " World");
            var result = await File.ReadAllTextAsync(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task AppendAllTextAsync_String_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello", Encoding.UTF8);
            await File.AppendAllTextAsync(tempFile, " World", Encoding.UTF8);
            var result = await File.ReadAllTextAsync(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void Move_WithOverwrite()
    {
        var tempFile1 = Path.GetTempFileName();
        var tempFile2 = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile1, "File1");
            File.WriteAllText(tempFile2, "File2");
            File.Move(tempFile1, tempFile2, overwrite: true);
            Assert.False(File.Exists(tempFile1));
            Assert.True(File.Exists(tempFile2));
            Assert.Equal("File1", File.ReadAllText(tempFile2));
        }
        finally
        {
            if (File.Exists(tempFile1))
                File.Delete(tempFile1);
            if (File.Exists(tempFile2))
                File.Delete(tempFile2);
        }
    }

    [Fact]
    public async Task ReadAllBytesAsync_Test()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            await File.WriteAllBytesAsync(tempFile, data);
            var result = await File.ReadAllBytesAsync(tempFile);
            Assert.Equal(data, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadAllLinesAsync_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" });
            var result = await File.ReadAllLinesAsync(tempFile);
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadAllLinesAsync_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" }, Encoding.UTF8);
            var result = await File.ReadAllLinesAsync(tempFile, Encoding.UTF8);
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadAllTextAsync_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello World");
            var result = await File.ReadAllTextAsync(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadAllTextAsync_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello World", Encoding.UTF8);
            var result = await File.ReadAllTextAsync(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadLinesAsync_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" });
            var result = new List<string>();
            await foreach (var line in File.ReadLinesAsync(tempFile))
            {
                result.Add(line);
            }
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task ReadLinesAsync_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" }, Encoding.UTF8);
            var result = new List<string>();
            await foreach (var line in File.ReadLinesAsync(tempFile, Encoding.UTF8))
            {
                result.Add(line);
            }
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllBytesAsync_ByteArray()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };
            await File.WriteAllBytesAsync(tempFile, data);
            var result = await File.ReadAllBytesAsync(tempFile);
            Assert.Equal(data, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllBytesAsync_ReadOnlyMemory()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            ReadOnlyMemory<byte> data = new byte[] { 1, 2, 3, 4, 5 };
            await File.WriteAllBytesAsync(tempFile, data);
            var result = await File.ReadAllBytesAsync(tempFile);
            Assert.Equal(data.ToArray(), result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllLinesAsync_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" });
            var result = await File.ReadAllLinesAsync(tempFile);
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllLinesAsync_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" }, Encoding.UTF8);
            var result = await File.ReadAllLinesAsync(tempFile, Encoding.UTF8);
            Assert.Equal(new[] { "line1", "line2", "line3" }, result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteAllText_ReadOnlySpan_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            ReadOnlySpan<char> data = "Hello World".AsSpan();
            File.WriteAllText(tempFile, data);
            var result = File.ReadAllText(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteAllText_ReadOnlySpan_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            ReadOnlySpan<char> data = "Hello World".AsSpan();
            File.WriteAllText(tempFile, data, Encoding.UTF8);
            var result = File.ReadAllText(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllTextAsync_String_NoEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello World");
            var result = await File.ReadAllTextAsync(tempFile);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public async Task WriteAllTextAsync_String_WithEncoding()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello World", Encoding.UTF8);
            var result = await File.ReadAllTextAsync(tempFile, Encoding.UTF8);
            Assert.Equal("Hello World", result);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
