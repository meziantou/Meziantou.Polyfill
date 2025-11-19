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

public class SystemThreadingTasksTests
{
        [Fact]
        public async Task Task_WaitAsync()
        {
            var tcs = new TaskCompletionSource<int>();
            _ = Task.Run(async () =>
            {
                await Task.Delay(500);
                tcs.TrySetResult(1);
            });
    
            var result = await tcs.Task.WaitAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }

}