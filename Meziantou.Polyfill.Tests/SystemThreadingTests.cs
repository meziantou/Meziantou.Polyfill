#pragma warning disable CA1307
#pragma warning disable CA1837
#pragma warning disable CA1849
#pragma warning disable CA2000
#pragma warning disable CA2264
#pragma warning disable CA5351
#pragma warning disable MA0001
#pragma warning disable MA0002
#pragma warning disable MA0015
#pragma warning disable MA0021
#pragma warning disable MA0074
#pragma warning disable MA0131
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

public class SystemThreadingTests
{
        [Fact]
        public async Task CancellationTokenSource_CancelAsync()
        {
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();
            Assert.True(cts.Token.IsCancellationRequested);
        }

        [Fact]
        public void Lock()
        {
            var instance = new Lock();
            Assert.False(instance.IsHeldByCurrentThread);
    
            RunOnThread(() =>
            {
                instance.Enter();
                Assert.True(instance.TryEnter());
                instance.Exit(); // exit tryenter
                TryEnterFromOtherThreadShouldFail();
                instance.Exit();
            });
    
            RunOnThread(() =>
            {
                using var scope = instance.EnterScope();
                TryEnterFromOtherThreadShouldFail();
            });
    
            RunOnThread(() =>
            {
                Assert.True(instance.TryEnter());
                Assert.True(instance.IsHeldByCurrentThread);
                instance.Exit();
            });
    
            RunOnThread(() =>
            {
                lock (instance)
                {
                    Assert.True(instance.IsHeldByCurrentThread);
                }
            });
    
            Assert.False(instance.IsHeldByCurrentThread);
    
            void RunOnThread(Action action)
            {
                var thread = new Thread(() => action());
                thread.Start();
                thread.Join();
            }
    
            void TryEnterFromOtherThreadShouldFail()
            {
                var thread = new Thread(() => Assert.False(instance.TryEnter()));
                thread.Start();
                thread.Join();
            }
        }

}