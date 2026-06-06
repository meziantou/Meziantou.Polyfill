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

public class SystemDiagnosticsTests
{
    [Fact]
    public async Task Process_WaitForExitAsync()
    {
        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "--version",
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };

        var process = Process.Start(psi);
        await process!.WaitForExitAsync();
        Assert.Equal(0, process.ExitCode);
    }

    [Fact]
    public void UnreachableException_DefaultConstructor()
    {
        var exception = new UnreachableException();
        Assert.NotNull(exception.Message);
        Assert.Contains("unreachable", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void UnreachableException_MessageConstructor()
    {
        var message = "Custom unreachable message";
        var exception = new UnreachableException(message);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void UnreachableException_MessageAndInnerExceptionConstructor()
    {
        var message = "Custom unreachable message";
        var innerException = new InvalidOperationException("Inner exception");
        var exception = new UnreachableException(message, innerException);
        Assert.Equal(message, exception.Message);
        Assert.Same(innerException, exception.InnerException);
    }

    [Fact]
    public void Stopwatch_GetElapsedTime_TwoTimestamps()
    {
        var start = Stopwatch.GetTimestamp();
        Thread.Sleep(15);
        var end = Stopwatch.GetTimestamp();

        var elapsed = Stopwatch.GetElapsedTime(start, end);
        Assert.True(elapsed.TotalMilliseconds >= 10, $"Expected >= 10ms, got {elapsed.TotalMilliseconds}ms");
        Assert.True(elapsed.TotalSeconds < 5, $"Expected < 5s, got {elapsed.TotalSeconds}s");
    }

    [Fact]
    public void Stopwatch_GetElapsedTime_SameTimestamp_ReturnsZero()
    {
        var timestamp = Stopwatch.GetTimestamp();
        var elapsed = Stopwatch.GetElapsedTime(timestamp, timestamp);
        Assert.Equal(TimeSpan.Zero, elapsed);
    }

    [Fact]
    public void Stopwatch_GetElapsedTime_StartingTimestamp()
    {
        var start = Stopwatch.GetTimestamp();
        Thread.Sleep(10);
        Assert.True(Stopwatch.GetElapsedTime(start) > TimeSpan.Zero);
    }

}
