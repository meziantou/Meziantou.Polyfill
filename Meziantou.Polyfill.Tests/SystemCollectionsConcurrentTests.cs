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

public class SystemCollectionsConcurrentTests
{
        [Fact]
        public void ConcurrentDictionary_GetOrAdd()
        {
            var dict = new ConcurrentDictionary<int, string>();
            var actual = dict.GetOrAdd(0, (key, arg) => arg, "arg");
            Assert.Equal("arg", actual);
        }

}