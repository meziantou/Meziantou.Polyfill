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
            FileName = Environment.OSVersion.Platform == PlatformID.Win32NT ? "ping.exe" : "ping",
            Arguments = Environment.OSVersion.Platform == PlatformID.Win32NT ? "127.0.0.1 -n 5" : "127.0.0.1 -c 5",
            CreateNoWindow = true,
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

}
