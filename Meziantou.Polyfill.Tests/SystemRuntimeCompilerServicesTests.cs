using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

public class SystemRuntimeCompilerServicesTests
{
    [Fact]
    public void CollectionBuilder()
    {
        CustomCollectionWithBuilder collection = ["a", "b"];
        Assert.Equal(["a", "b"], collection);
    }

}

[CollectionBuilder(typeof(CustomCollectionWithBuilder), "Create")]
file sealed class CustomCollectionWithBuilder : IEnumerable<string>
{
    private readonly string[] _data;

    private CustomCollectionWithBuilder(string[] data) => _data = data;

    public static CustomCollectionWithBuilder Create(ReadOnlySpan<string> data)
    {
        return new CustomCollectionWithBuilder(data.ToArray());
    }

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var item in _data)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
