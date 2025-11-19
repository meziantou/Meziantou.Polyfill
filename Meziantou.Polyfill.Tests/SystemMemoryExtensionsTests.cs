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

public class SystemMemoryExtensionsTests
{
    [Fact]
    public void CommonPrefixLength()
    {
        Assert.Equal(0, ((Span<int>)[0]).CommonPrefixLength([1]));
        Assert.Equal(1, ((Span<int>)[0]).CommonPrefixLength([0]));
        Assert.Equal(2, ((Span<int>)[0, 1]).CommonPrefixLength([0, 1, 2]));
    }

}
