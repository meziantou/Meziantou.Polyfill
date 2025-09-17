#pragma warning disable CA1307
#pragma warning disable CA1849
#pragma warning disable CA2000
#pragma warning disable MA0001
#pragma warning disable MA0002
#pragma warning disable MA0021
#pragma warning disable MA0074
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class UnitTest1
{
    [Fact]
    public void AttributesAreAvailable()
    {
        _ = new AllowNullAttribute();
        _ = new DisallowNullAttribute();
        _ = new DoesNotReturnAttribute();
        _ = new DoesNotReturnIfAttribute(true);
        _ = new DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes.All);
        _ = new DynamicDependencyAttribute("");
        _ = new MaybeNullAttribute();
        _ = new MaybeNullWhenAttribute(true);
        _ = new MemberNotNullAttribute("");
        _ = new MemberNotNullWhenAttribute(false);
        _ = new NotNullAttribute();
        _ = new NotNullIfNotNullAttribute("");
        _ = new NotNullWhenAttribute(false);
        _ = new RequiresAssemblyFilesAttribute();
        _ = new RequiresDynamicCodeAttribute("");
        _ = new RequiresUnreferencedCodeAttribute("");
        _ = new SetsRequiredMembersAttribute();
        _ = new StringSyntaxAttribute("");
        _ = new UnconditionalSuppressMessageAttribute("", "");
        _ = new UnscopedRefAttribute();
        _ = new StackTraceHiddenAttribute();
        _ = new AsyncMethodBuilderAttribute(typeof(UnitTest1));
        _ = new CallerArgumentExpressionAttribute("");
        _ = new CompilerFeatureRequiredAttribute("");
        _ = new DisableRuntimeMarshallingAttribute();
        _ = new InterpolatedStringHandlerArgumentAttribute("");
        _ = new InterpolatedStringHandlerAttribute();
        _ = new ModuleInitializerAttribute();
        _ = new RequiredMemberAttribute();
        _ = new SkipLocalsInitAttribute();
        _ = new SuppressGCTransitionAttribute();
        _ = new UnmanagedCallersOnlyAttribute();
        _ = new ObsoletedOSPlatformAttribute("");
        _ = new RequiresPreviewFeaturesAttribute();
        _ = new SupportedOSPlatformAttribute("");
        _ = new SupportedOSPlatformGuardAttribute("");
        _ = new TargetPlatformAttribute("");
        _ = new UnsupportedOSPlatformAttribute("");
        _ = new UnsupportedOSPlatformGuardAttribute("");
        _ = new CollectionBuilderAttribute(typeof(string), "");
        _ = new ExperimentalAttribute("test");
        _ = new OverloadResolutionPriorityAttribute(1);

        _ = typeof(IsExternalInit);
    }

    [Fact]
    public void System_Collections_Generic_KeyValuePair_2_Deconstruct()
    {
        var data = new KeyValuePair<string, object>("test", 2);
        var (key, value) = data;
        Assert.Equal("test", key);
        Assert.Equal(2, value);
    }

    [Fact]
    public void System_Collections_Generic_Queue_1_TryDequeue()
    {
        var queue = new Queue<int>();
        Assert.False(queue.TryDequeue(out _));

        queue.Enqueue(1);
        queue.Enqueue(2);
        Assert.True(queue.TryDequeue(out var item1));
        Assert.True(queue.TryDequeue(out var item2));
        Assert.False(queue.TryDequeue(out _));

        Assert.Equal(1, item1);
        Assert.Equal(2, item2);
    }

    [Fact]
    public void ReadOnlySpan_Contains()
    {
        ReadOnlySpan<int> span = [1, 2];
        Assert.True(span.Contains(1));
        Assert.True(span.Contains(2));
        Assert.False(span.Contains(3));
    }

    [Fact]
    public void Span_Contains()
    {
        Span<int> span = [1, 2];
        Assert.True(span.Contains(1));
        Assert.True(span.Contains(2));
        Assert.False(span.Contains(3));
    }

    [Fact]
    public void String_Contains_Char()
    {
        Assert.True("test".Contains('t'));
        Assert.False("test".Contains('T'));
    }

    [Fact]
    public void String_Contains_Char_StringComparison()
    {
        Assert.True("test".Contains('t', StringComparison.Ordinal));
        Assert.True("test".Contains('T', StringComparison.OrdinalIgnoreCase));
        Assert.False("test".Contains('T', StringComparison.Ordinal));
    }

    [Fact]
    public void String_Contains()
    {
        Assert.True("test".Contains("tes", StringComparison.Ordinal));
        Assert.True("test".Contains("TEs", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void String_CopyTo()
    {
        Assert.Throws<ArgumentException>(() => "test".CopyTo(new char[1].AsSpan()));

        var data = new char[4];
        "test".CopyTo(data.AsSpan());
        Assert.Equal("test".ToCharArray(), data);
    }

    [Fact]
    public void String_EndsWith_Char()
    {
        Assert.True("test".EndsWith('t'));
        Assert.False("test".EndsWith('T'));
    }

    [Fact]
    public void String_GetHashCode()
    {
        Assert.Equal("test".GetHashCode(), "test".GetHashCode(StringComparison.Ordinal));
        Assert.Equal(StringComparer.OrdinalIgnoreCase.GetHashCode("Test"), "test".GetHashCode(StringComparison.OrdinalIgnoreCase));

    }

    [Fact]
    public void String_Replace()
    {
        Assert.Equal("sample", "samDumMyle2".Replace("dummy", "p", StringComparison.OrdinalIgnoreCase).Replace("2", "", StringComparison.Ordinal));
        Assert.Throws<ArgumentException>(() => "".Replace("", "dummy", StringComparison.OrdinalIgnoreCase));
        Assert.Throws<ArgumentNullException>(() => "".Replace(null!, "dummy", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void String_Split()
    {
        Assert.Equal<string[]>(["a", "b", "c"], "a;b;c".Split(';'));
        Assert.Equal<string[]>(["a", " b", "c"], "a; b;c".Split(';'));
        Assert.Equal<string[]>(["a", "", "b", "c"], "a;;b;c".Split(';', StringSplitOptions.None));
        Assert.Equal<string[]>(["a", "b", "c"], "a;;b;c".Split(';', StringSplitOptions.RemoveEmptyEntries));
        Assert.Equal<string[]>(["a", " b;c"], "a; b;c".Split(';', 2, StringSplitOptions.None));
    }

    [Fact]
    public void String_StartsWith_Char()
    {
        Assert.True("test".StartsWith('t'));
        Assert.False("test".StartsWith('T'));
    }

    [Fact]
    public void String_TryCopyTo()
    {
        Assert.False("test".TryCopyTo(new char[1].AsSpan()));

        var data = new char[4];
        Assert.True("test".TryCopyTo(data.AsSpan()));
        Assert.Equal("test".ToCharArray(), data);
    }

    [Fact]
    public void StringBuilder_Append_ReadonlySpan()
    {
        Assert.Equal("test", new StringBuilder().Append("test".AsSpan()).ToString());
    }

    [Fact]
    public void StringBuilder_Append_ReadonlyMemory()
    {
        Assert.Equal("test", new StringBuilder().Append("test".AsMemory()).ToString());
    }

    [Fact]
    public async Task CancellationTokenSource_CancelAsync()
    {
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        Assert.True(cts.Token.IsCancellationRequested);
    }

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public async Task StreamWriter_WriteAsync()
    {
        using var sr = new System.IO.StringWriter();
        await sr.WriteAsync("test".AsMemory(), CancellationToken.None);
        Assert.Equal("test", sr.ToString());
    }
#endif

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public void StreamWriter_Write()
    {
        using var ms = new MemoryStream();
        ms.Write([1, 2]);
        Assert.Equal([1, 2], ms.ToArray());
    }
#endif

    [Fact]
    public async Task StreamReader_ReadToEndAsync()
    {
        using var sr = new StringReader("test");
        var result = await sr.ReadToEndAsync(CancellationToken.None);
        Assert.Equal("test", result);
    }

#if NET461_OR_GREATER || NETCOREAPP
    [Fact]
    public async Task StreamReader_ReadAsync()
    {
        using var sr = new StringReader("test");
        var buffer = new char[2];
        var result = await sr.ReadAsync(buffer.AsMemory(), CancellationToken.None);
        Assert.Equal(2, result);
        Assert.Equal("te", new string(buffer));
    }

    [Fact]
    public async Task StreamReader_ReadAsync2()
    {
        using var sr = new MemoryStream([3, 4, 5]);
        var buffer = new byte[3];
        buffer[0] = 1;
        var result = await sr.ReadAsync(buffer.AsMemory()[1..], CancellationToken.None);
        Assert.Equal(2, result);
        Assert.Equal([1, 3, 4], buffer);
    }
#endif

    [Fact]
    public void String_IndexOf_Char_StringComparison()
    {
        Assert.Equal(2, "test".IndexOf('S', StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void HashCode_Combine()
    {
        Assert.Equal(HashCode.Combine("foo", "bar"), HashCode.Combine("foo", "bar"));
        Assert.NotEqual(0, HashCode.Combine("foo", "bar"));
    }

    [Fact]
    public void Enumerable_Order()
    {
        Assert.Equal([1, 2, 3], new[] { 2, 1, 3 }.Order());
        Assert.Equal([1, 2, 3], new[] { 2, 1, 3 }.Order(Comparer<int>.Default));
    }

    [Fact]
    public void Enumerable_OrderDescending()
    {
        Assert.Equal([3, 2, 1], new[] { 2, 1, 3 }.OrderDescending());
        Assert.Equal([3, 2, 1], new[] { 2, 1, 3 }.OrderDescending(Comparer<int>.Default));
    }

    [Fact]
    public void Enumerable_DistinctBy()
    {
        Assert.Equal([2, 1], new[] { 2, 1, 1 }.DistinctBy(_ => _));
        Assert.Equal([2, 1], new[] { 2, 1, 1 }.DistinctBy(_ => _, EqualityComparer<int>.Default));
    }

    [Fact]
    public void String_ReplaceLineEndings()
    {
        Assert.Equal("\n\n\n", "\r\n\n\f".ReplaceLineEndings("\n"));
    }

    [Fact]
    public void String_ReplaceLineEndings2()
    {
        Assert.Equal("\r\n\r\n\r\n", "\r\n\n\f".ReplaceLineEndings("\r\n"));
    }

    [Fact]
    public void Enumerable_Zip()
    {
        Assert.Equal(new[] { ('a', 'b') }, "a".Zip("b"));
    }

    [Fact]
    public void Enumerable_CountBy()
    {
        var collection = new[] { 1, 1, 2, 3, 4, 3, 3 };
        var expected = new[] {
            new KeyValuePair<int, int>(1, 2),
            new KeyValuePair<int, int>(2, 1),
            new KeyValuePair<int, int>(3, 3),
            new KeyValuePair<int, int>(4, 1),
        };
        Assert.Equal(expected, collection.CountBy(item => item));
    }

    [Fact]
    public void Enumerable_AggregateBy_Seed()
    {
        var collection = new[] { 1, 2, 2, 3 };
        var expected = new[] {
            new KeyValuePair<int, int>(1, 1),
            new KeyValuePair<int, int>(2, 4),
            new KeyValuePair<int, int>(3, 3),
        };
        Assert.Equal(expected, collection.AggregateBy(item => item, seed: 0, (acc, item) => acc + item));
    }

    [Fact]
    public void Enumerable_AggregateBy_SeedSelector()
    {
        var collection = new[] { 1, 2, 2, 3 };
        var expected = new[] {
            new KeyValuePair<int, int>(1, 2),
            new KeyValuePair<int, int>(2, 5),
            new KeyValuePair<int, int>(3, 4),
        };
        Assert.Equal(expected, collection.AggregateBy(item => item, seedSelector: item => 1, (acc, item) => acc + item));
    }

    [Fact]
    public void ValueTuple()
    {
        Assert.Equal((1, 2, 3, 4, 5, 6, 7, 8, 9, 10), (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
    }

    [Fact]
    public void ConcurrentDictionary_GetOrAdd()
    {
        var dict = new ConcurrentDictionary<int, string>();
        var actual = dict.GetOrAdd(0, (key, arg) => arg, "arg");
        Assert.Equal("arg", actual);
    }

    [Fact]
    public async Task StreamReader_ReadLineAsync()
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes("ab\ncd"));
        using var reader = new StreamReader(ms, Encoding.UTF8);

        Assert.Equal("ab", await reader.ReadLineAsync());
        Assert.Equal("cd", await reader.ReadLineAsync(CancellationToken.None));
        Assert.Null(await reader.ReadLineAsync(CancellationToken.None));
    }

    [Fact]
    public void Stream_ReadAtLeast()
    {
        using var ms = new MemoryStream([1, 2, 3, 4]);

        var buffer = new byte[4];
        Assert.Equal(4, ms.ReadAtLeast(buffer.AsSpan(), 4));
        Assert.Equal([1, 2, 3, 4], buffer);
    }

    [Fact]
    public async Task Stream_ReadAtLeastAsync()
    {
        using var ms = new MemoryStream([1, 2, 3, 4]);

        var buffer = new byte[4];
        Assert.Equal(4, await ms.ReadAtLeastAsync(buffer.AsMemory(), 4));
        Assert.Equal([1, 2, 3, 4], buffer);
    }

    [Fact]
    public void String_AsSpan()
    {
        var actual = "test".AsSpan(1, 2);
        Assert.Equal("es", actual.ToString());
    }

    [Fact]
    public void Enumerable_MaxBy()
    {
        Assert.Equal("ab", new[] { "a", "ab", "b" }.MaxBy(a => a.Length));
        Assert.Equal("ab", new[] { "a", "ab", "b" }.MaxBy(a => a.Length, Comparer<int>.Default));
    }

    [Fact]
    public void Enumerable_MinBy()
    {
        Assert.Equal("a", new[] { "a", "ab", "b" }.MinBy(a => a.Length));
        Assert.Equal("a", new[] { "a", "ab", "b" }.MinBy(a => a.Length, Comparer<int>.Default));
    }

    [Fact]
    public void Enumerable_ToDictionary()
    {
        Dictionary<string, int> dict = new() { ["a"] = 1, ["b"] = 2 };

        var copied = (Dictionary<string, int>) ((IEnumerable<KeyValuePair<string, int>>) dict).ToDictionary();
        Assert.Equal(dict, copied);
        Assert.Equal(dict.Comparer, copied.Comparer);

        var caseInsensitive = (Dictionary<string, int>) ((IEnumerable<KeyValuePair<string, int>>) dict).ToDictionary(StringComparer.OrdinalIgnoreCase);
        Assert.Equal(dict, caseInsensitive);
        Assert.Equal(StringComparer.OrdinalIgnoreCase, caseInsensitive.Comparer);
    }

    [Fact]
    public void Enumerable_ToHashSet()
    {
        Assert.Equal(new[] { "a", "b" }, new[] { "a", "b", "a" }.ToHashSet());
        Assert.Equal(new[] { "a", "b" }, new[] { "a", "b", "A" }.ToHashSet(StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public void IList_AsReadOnly()
    {
        var list = new List<int>() { 1, 2 };
        ReadOnlyCollection<int> result = list.AsReadOnly();
        Assert.Equal(list, result);
    }

    [Fact]
    public void IDictionary_AsReadOnly()
    {
        IDictionary<int, string> dict = new Dictionary<int, string>
        {
            { 1, "a" },
            { 2, "b" },
        };

        ReadOnlyDictionary<int, string> result = dict.AsReadOnly();
        Assert.Equal(dict, result);
    }

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
    public void PriorityQueueTests()
    {
        var queue = new PriorityQueue<string, int>();
        queue.Enqueue("1", 1);
        queue.Enqueue("0", 0);
        queue.Enqueue("2", 2);

        Assert.Equal("0", queue.Dequeue());
        Assert.Equal("1", queue.Dequeue());
        Assert.Equal("2", queue.Dequeue());
    }

    [Fact]
    public void ReferenceEqualityComparerTests()
    {
        Assert.False(ReferenceEqualityComparer.Instance.Equals(new object(), new object()));
        Assert.NotEqual(ReferenceEqualityComparer.Instance.GetHashCode(new object()), ReferenceEqualityComparer.Instance.GetHashCode(new object()));

        var obj = new object();
        Assert.True(ReferenceEqualityComparer.Instance.Equals(obj, obj));
        Assert.Equal(ReferenceEqualityComparer.Instance.GetHashCode(obj), ReferenceEqualityComparer.Instance.GetHashCode(obj));
    }

    [Fact]
    public void ReadOnlyDictionary_GetValueOrDefaultTests()
    {
        IReadOnlyDictionary<int, int> dictionary = new Dictionary<int, int>
        {
            [1] = 10
        };

        Assert.Equal(10, dictionary.GetValueOrDefault(1));
        Assert.Equal(10, dictionary.GetValueOrDefault(1, -1));

        // key not present
        Assert.Equal(0, dictionary.GetValueOrDefault(100));
        Assert.Equal(-1, dictionary.GetValueOrDefault(100, -1));
    }

    [Fact]
    public async Task ReadOnlyMemoryContent()
    {
        using var content = new ReadOnlyMemoryContent(new byte[] { 1, 2 });
        var ms = new MemoryStream();
        await content.CopyToAsync(ms);

        Assert.Equal([1, 2], ms.ToArray());
    }

    [Fact]
    public void HttpContent_ReadAsStream()
    {
        using var content = new ByteArrayContent([1, 2]);
        var stream = content.ReadAsStream();

        var streamContent = new MemoryStream();
        stream.CopyTo(streamContent);

        Assert.Equal([1, 2], streamContent.ToArray());
    }

    [Fact]
    public void UdpClient()
    {
        int port = 1024;

        UdpClient CreateUdpClient()
        {
            while (true)
            {
                try
                {
                    return new UdpClient(port);
                }
                catch
                {
                    port++;
                    if (port >= ushort.MaxValue)
                        throw;
                }
            }
        }

        using UdpClient client = CreateUdpClient();
        using UdpClient server = new();

        ReadOnlySpan<byte> data = [1, 2, 3];
        server.Send(data, "localhost", port);
        IPEndPoint endpoint = new(IPAddress.Any, 0);
        var result = client.Receive(ref endpoint);
        Assert.Equal(data.ToArray(), result);
    }

    [Fact]
    public async Task UdpClientAsync()
    {
        int port = 1024;

        UdpClient CreateUdpClient()
        {
            while (true)
            {
                try
                {
                    return new UdpClient(port);
                }
                catch
                {
                    port++;
                    if (port >= ushort.MaxValue)
                        throw;
                }
            }
        }

        using UdpClient client = CreateUdpClient();
        using UdpClient server = new();

        ReadOnlyMemory<byte> data = new([1, 2, 3]);
        await server.SendAsync(data, "localhost", port);
        IPEndPoint endpoint = new(IPAddress.Any, 0);
        var result = client.Receive(ref endpoint);
        Assert.Equal(data.ToArray(), result);
    }

    [Fact]
    public void Encoding_GetString()
    {
        var str = Encoding.UTF8.GetString((ReadOnlySpan<byte>)Encoding.UTF8.GetBytes("sample").AsSpan());
        Assert.Equal("sample", str);
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_ObjectArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", 1, 2, 3);
        Assert.Equal("1, 2, 3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_StringArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", "1", "2", "3");
        Assert.Equal("1, 2, 3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_String_IEnumerable()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(", ", Enumerable());
        Assert.Equal("1, 2, 3", sb.ToString());

        static IEnumerable<string> Enumerable()
        {
            yield return "1";
            yield return "2";
            yield return "3";
        }
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_ObjectArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', 1, 2, 3);
        Assert.Equal("1,2,3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_StringArray()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', "1", "2", "3");
        Assert.Equal("1,2,3", sb.ToString());
    }

    [Fact]
    public void StringBuilder_AppendJoin_Char_IEnumerable()
    {
        var sb = new StringBuilder();
        sb.AppendJoin(',', Enumerable());
        Assert.Equal("1,2,3", sb.ToString());

        static IEnumerable<string> Enumerable()
        {
            yield return "1";
            yield return "2";
            yield return "3";
        }
    }

    [Fact]
    public void StringBuilder_Replace_ReadOnlySpanChar_ReadOnlySpanChar()
    {
        var sb = new StringBuilder("abcd");
        sb.Replace("bc".AsSpan(), "zy".AsSpan());
        Assert.Equal("azyd", sb.ToString());
    }

    [Fact]
    public void StringBuilder_Replace_ReadOnlySpanChar_ReadOnlySpanChar_Int32_Int32()
    {
        var sb = new StringBuilder("abcdbcbc");
        sb.Replace("bc".AsSpan(), "zy".AsSpan(), 2, 5);
        Assert.Equal("abcdzybc", sb.ToString());
    }

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

    [Fact]
    public void Span_ContainsAny_2()
    {
        Span<int> span = [0, 1, 2];
        Assert.False(span.ContainsAny(10, 11));
        Assert.True(span.ContainsAny(10, 1));
        Assert.True(span.ContainsAny(1, 10));
    }

    [Fact]
    public void Span_ContainsAny_3()
    {
        Span<int> span = [0, 1, 2];
        Assert.False(span.ContainsAny(10, 11, 12));
        Assert.True(span.ContainsAny(1, 10, 11));
        Assert.True(span.ContainsAny(10, 1, 11));
        Assert.True(span.ContainsAny(10, 11, 1));
    }

    [Fact]
    public void Span_ContainsAny_ReadOnlySpan()
    {
        Span<int> span = [0, 1, 2];

        ReadOnlySpan<int> search = [10, 11, 12];
        Assert.False(span.ContainsAny(search));

        search = [1, 11, 12];
        Assert.True(span.ContainsAny(search));

        search = [10, 1, 12];
        Assert.True(span.ContainsAny(search));

        search = [10, 11, 1];
        Assert.True(span.ContainsAny(search));

        search = [10, 11, 12, 1];
        Assert.True(span.ContainsAny(search));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_1()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0));
        Assert.True(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(1));
        Assert.True(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_2()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0, 1));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0, 1));
        Assert.True(((ReadOnlySpan<int>)[0, 1, 2]).ContainsAnyExcept(0, 1));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_3()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((ReadOnlySpan<int>)[0, 1, 2]).ContainsAnyExcept(0, 1, 2));
        Assert.True(((ReadOnlySpan<int>)[0, 1, 2, 3]).ContainsAnyExcept(0, 1, 2));
    }

    [Fact]
    public void ReadOnlySpan_ContainsAnyExcept_ReadOnlySpan()
    {
        Assert.False(((ReadOnlySpan<int>)[0]).ContainsAnyExcept([0]));
        Assert.False(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept([0, 1, 2]));
        Assert.True(((ReadOnlySpan<int>)[0, 1]).ContainsAnyExcept([0]));
    }

    [Fact]
    public void Span_ContainsAnyExcept_1()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0));
        Assert.True(((Span<int>)[0]).ContainsAnyExcept(1));
        Assert.True(((Span<int>)[0, 1]).ContainsAnyExcept(0));
    }

    [Fact]
    public void Span_ContainsAnyExcept_2()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0, 1));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept(0, 1));
        Assert.True(((Span<int>)[0, 1, 2]).ContainsAnyExcept(0, 1));
    }

    [Fact]
    public void Span_ContainsAnyExcept_3()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept(0, 1, 2));
        Assert.False(((Span<int>)[0, 1, 2]).ContainsAnyExcept(0, 1, 2));
        Assert.True(((Span<int>)[0, 1, 2, 3]).ContainsAnyExcept(0, 1, 2));
    }

    [Fact]
    public void Span_ContainsAnyExcept_ReadOnlySpan()
    {
        Assert.False(((Span<int>)[0]).ContainsAnyExcept([0]));
        Assert.False(((Span<int>)[0, 1]).ContainsAnyExcept([0, 1, 2]));
        Assert.True(((Span<int>)[0, 1]).ContainsAnyExcept([0]));
    }

    [Fact]
    public void CommonPrefixLength()
    {
        Assert.Equal(0, ((Span<int>)[0]).CommonPrefixLength([1]));
        Assert.Equal(1, ((Span<int>)[0]).CommonPrefixLength([0]));
        Assert.Equal(2, ((Span<int>)[0, 1]).CommonPrefixLength([0, 1, 2]));
    }

    [Fact]
    public void Enumerable_Index()
    {
        Assert.Equal([(0, "a"), (1, "b")], (new string[] { "a", "b" }).Index());
    }

    [Fact]
    public void TimeSpan_Multiply()
    {
        Assert.Equal(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1).Multiply(2));
    }

    [Fact]
    public async Task HttpContent_ReadAsStringAsync()
    {
        var content = new StringContent("dummy");
        Assert.Equal("dummy", await content.ReadAsStringAsync(CancellationToken.None));
    }

    [Fact]
    public void Dictionary_Remove()
    {
        var dict = new Dictionary<int, int>();
        Assert.False(dict.Remove(1, out _));

        dict.Add(1, 2);
        Assert.True(dict.Remove(1, out var value));
        Assert.Equal(2, value);
    }

    [Fact]
    public void IEnumerable_IntersectBy()
    {
        IEnumerable<int> a = [1, 2, 3];
        IEnumerable<int> b = [2, 3, 4];
        var result = a.IntersectBy(b, i => i);
        Assert.Equal([2, 3], result);
    }

    [Fact]
    public void IEnumerable_IntersectBy_Comparer()
    {
        IEnumerable<int> a = [1, 2, 3];
        IEnumerable<int> b = [2, 3, 4];
        var result = a.IntersectBy(b, i => i, EqualityComparer<int>.Default);
        Assert.Equal([2, 3], result);
    }

    [Fact]
    public void IEnumerable_UnionBy()
    {
        IEnumerable<int> a = [1, 2, 3];
        IEnumerable<int> b = [2, 3, 4];
        var result = a.UnionBy(b, i => i);
        Assert.Equal([1, 2, 3, 4], result);
    }

    [Fact]
    public void IEnumerable_UnionBy_Comparer()
    {
        IEnumerable<int> a = [1, 2, 3];
        IEnumerable<int> b = [2, 3, 4];
        var result = a.UnionBy(b, i => i, EqualityComparer<int>.Default);
        Assert.Equal([1, 2, 3, 4], result);
    }

    [Fact]
    public void Type_GetConstructor()
    {
        Assert.NotNull(typeof(Exception).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            new Type[]
            {
                typeof(SerializationInfo),
                typeof(StreamingContext)
            })
        );
        Assert.NotNull(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
        Assert.Null(typeof(Random).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { typeof(IntPtr) }));
    }

    [Fact]
    public void Type_GetMethod()
    {
        Assert.NotNull(typeof(Exception).GetMethod("get_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
        Assert.Null(typeof(Exception).GetMethod("set_Message", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>()));
    }

    [Fact]
    public void Type_IsAssignableTo()
    {
        Assert.True(typeof(string).IsAssignableTo(typeof(object)));
        Assert.True(typeof(string).IsAssignableTo(typeof(string)));
        Assert.False(typeof(string).IsAssignableTo(typeof(int)));
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

    [Fact]
    public void ToBlockingEnumerable()
    {
        Assert.Equal(["a", "b"], CustomAsyncEnumerable().ToBlockingEnumerable());

        async IAsyncEnumerable<string> CustomAsyncEnumerable()
        {
            yield return "a";
            await Task.Yield();
            yield return "b";
        }
    }

    [Fact]
    public void CollectionBuilder()
    {
        CustomCollectionWithBuilder collection = ["a", "b"];
        Assert.Equal(["a", "b"], collection);
    }

    [CollectionBuilder(typeof(CustomCollectionWithBuilder), "Create")]
    private sealed class CustomCollectionWithBuilder : IEnumerable<string>
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
}
