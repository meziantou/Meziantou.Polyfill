using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public void Process_Kill_EntireProcessTree()
    {
        // The root process starts a child process and writes the child process id on the standard output
        var psi = OperatingSystem.IsWindows()
            ? new ProcessStartInfo("powershell.exe", "-NoProfile -NonInteractive -Command \"$p = Start-Process -FilePath ping.exe -ArgumentList '-n 120 127.0.0.1' -WindowStyle Hidden -PassThru; [Console]::Out.WriteLine($p.Id); $p.WaitForExit()\"")
            : new ProcessStartInfo("/bin/sh", "-c \"sleep 120 & echo $!; wait\"");
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;

        using var process = Process.Start(psi)!;
        try
        {
            var childProcessId = int.Parse(process.StandardOutput.ReadLine()!.Trim(), CultureInfo.InvariantCulture);
            Assert.True(IsRunning(childProcessId));

            process.Kill(entireProcessTree: true);

            Assert.True(process.WaitForExit(30_000));
            var stopwatch = Stopwatch.StartNew();
            while (IsRunning(childProcessId) && stopwatch.Elapsed < TimeSpan.FromSeconds(30))
            {
                Thread.Sleep(50);
            }

            Assert.False(IsRunning(childProcessId));
        }
        finally
        {
            if (!process.HasExited)
            {
                process.Kill();
            }
        }

        static bool IsRunning(int processId)
        {
            try
            {
                using var p = Process.GetProcessById(processId);
                return !p.HasExited;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }

    [Fact]
    public void Process_Kill_EntireProcessTree_CurrentProcess()
    {
        using var process = Process.GetCurrentProcess();
        Assert.Throws<InvalidOperationException>(() => process.Kill(entireProcessTree: true));
    }

    [Fact]
    public void Process_Kill_EntireProcessTree_False()
    {
        var psi = OperatingSystem.IsWindows()
            ? new ProcessStartInfo("ping.exe", "-n 120 127.0.0.1")
            : new ProcessStartInfo("sleep", "120");
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;

        using var process = Process.Start(psi)!;
        process.Kill(entireProcessTree: false);
        Assert.True(process.WaitForExit(30_000));
    }

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
        var end = start + (Stopwatch.Frequency / 10); // Add exactly 100ms of ticks

        var elapsed = Stopwatch.GetElapsedTime(start, end);
        Assert.Equal(TimeSpan.FromMilliseconds(100), elapsed);
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
