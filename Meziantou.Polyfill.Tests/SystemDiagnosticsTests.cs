using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
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

}