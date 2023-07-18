// Polyfills: 101
#nullable enable
using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Meziantou.Polyfill;

internal readonly partial struct Members : IEquatable<Members>
{
private readonly ulong _bits0 = 0uL;
private readonly ulong _bits1 = 0uL;
private readonly PolyfillOptions _options;
private readonly bool _hasSpanOfT;
private readonly bool _hasReadOnlySpanOfT;
private readonly bool _hasMemoryOfT;
private readonly bool _hasReadOnlyMemoryOfT;
private readonly bool _hasValueTask;
private readonly bool _hasValueTaskOfT;
private readonly bool _hasImmutableArrayOfT;
public Members(Compilation compilation, PolyfillOptions options)
{
    _options = options;
    _hasSpanOfT = compilation.GetTypeByMetadataName("System.Span`1") != null;
    _hasReadOnlySpanOfT = compilation.GetTypeByMetadataName("System.ReadOnlySpan`1") != null;
    _hasMemoryOfT = compilation.GetTypeByMetadataName("System.Memory`1") != null;
    _hasReadOnlyMemoryOfT = compilation.GetTypeByMetadataName("System.ReadOnlyMemory`1") != null;
    _hasValueTask = compilation.GetTypeByMetadataName("System.Threading.Tasks.ValueTask") != null;
    _hasValueTaskOfT = compilation.GetTypeByMetadataName("System.Threading.Tasks.ValueTask`1") != null;
    _hasImmutableArrayOfT = compilation.GetTypeByMetadataName("System.Collections.Immutable.ImmutableArray`1") != null;
    if (IncludeMember(compilation, options, "M:System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd``1(`0,System.Func{`0,``0,`1},``0)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.GetOrAdd``3(System.Collections.Concurrent.ConcurrentDictionary{``0,``1},``0,System.Func{``0,``2,``1},``2)~``1")))
        _bits0 = _bits0 | 1uL;
    if (IncludeMember(compilation, options, "M:System.Collections.Generic.KeyValuePair`2.Deconstruct(`0@,`1@)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Deconstruct``2(System.Collections.Generic.KeyValuePair{``0,``1},``0@,``1@)")))
        _bits0 = _bits0 | 2uL;
    if (IncludeMember(compilation, options, "M:System.Collections.Generic.Queue`1.TryDequeue(`0@)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.TryDequeue``1(System.Collections.Generic.Queue{``0},``0@)~System.Boolean")))
        _bits0 = _bits0 | 4uL;
    if (_hasReadOnlySpanOfT && _hasImmutableArrayOfT && IncludeMember(compilation, options, "M:System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Int32,System.Int32)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.AsSpan``1(System.Collections.Immutable.ImmutableArray{``0},System.Int32,System.Int32)~System.ReadOnlySpan{``0}")))
        _bits0 = _bits0 | 8uL;
    if (_hasReadOnlySpanOfT && _hasImmutableArrayOfT && IncludeMember(compilation, options, "M:System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Range)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.AsSpan``1(System.Collections.Immutable.ImmutableArray{``0},System.Range)~System.ReadOnlySpan{``0}")))
        _bits0 = _bits0 | 16uL;
    if (IncludeMember(compilation, options, "M:System.Diagnostics.Process.WaitForExitAsync(System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.WaitForExitAsync(System.Diagnostics.Process,System.Threading.CancellationToken)~System.Threading.Tasks.Task") && IncludeMember(compilation, options, "M:PolyfillExtensions.TaskCompletionSourceWithCancellation`1.WaitWithCancellationAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task{`0}")))
        _bits0 = _bits0 | 32uL;
    if (_hasSpanOfT && IncludeMember(compilation, options, "M:System.IO.Stream.Read(System.Span{System.Byte})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Read(System.IO.Stream,System.Span{System.Byte})~System.Int32")))
        _bits0 = _bits0 | 64uL;
    if (_hasMemoryOfT && _hasValueTaskOfT && IncludeMember(compilation, options, "M:System.IO.Stream.ReadAsync(System.Memory{System.Byte},System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadAsync(System.IO.Stream,System.Memory{System.Byte},System.Threading.CancellationToken)~System.Threading.Tasks.ValueTask{System.Int32}")))
        _bits0 = _bits0 | 128uL;
    if (_hasSpanOfT && IncludeMember(compilation, options, "M:System.IO.Stream.ReadAtLeast(System.Span{System.Byte},System.Int32,System.Boolean)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadAtLeast(System.IO.Stream,System.Span{System.Byte},System.Int32,System.Boolean)~System.Int32")))
        _bits0 = _bits0 | 256uL;
    if (_hasMemoryOfT && _hasValueTaskOfT && IncludeMember(compilation, options, "M:System.IO.Stream.ReadAtLeastAsync(System.Memory{System.Byte},System.Int32,System.Boolean,System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadAtLeastAsync(System.IO.Stream,System.Memory{System.Byte},System.Int32,System.Boolean,System.Threading.CancellationToken)~System.Threading.Tasks.ValueTask{System.Int32}")))
        _bits0 = _bits0 | 512uL;
    if (IncludeMember(compilation, options, "M:System.IO.StreamReader.ReadLineAsync()") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadLineAsync(System.IO.StreamReader)~System.Threading.Tasks.Task{System.String}")))
        _bits0 = _bits0 | 1024uL;
    if (_hasValueTaskOfT && IncludeMember(compilation, options, "M:System.IO.StreamReader.ReadLineAsync(System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadLineAsync(System.IO.StreamReader,System.Threading.CancellationToken)~System.Threading.Tasks.ValueTask{System.String}")))
        _bits0 = _bits0 | 2048uL;
    if (_hasMemoryOfT && _hasValueTaskOfT && IncludeMember(compilation, options, "M:System.IO.TextReader.ReadAsync(System.Memory{System.Char},System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadAsync(System.IO.TextReader,System.Memory{System.Char},System.Threading.CancellationToken)~System.Threading.Tasks.ValueTask{System.Int32}")))
        _bits0 = _bits0 | 4096uL;
    if (IncludeMember(compilation, options, "M:System.IO.TextReader.ReadToEndAsync(System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReadToEndAsync(System.IO.TextReader,System.Threading.CancellationToken)~System.Threading.Tasks.Task{System.String}")))
        _bits0 = _bits0 | 8192uL;
    if (_hasReadOnlyMemoryOfT && _hasValueTask && IncludeMember(compilation, options, "M:System.IO.TextWriter.WriteAsync(System.ReadOnlyMemory{System.Char},System.Threading.CancellationToken)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.WriteAsync(System.IO.TextWriter,System.ReadOnlyMemory{System.Char},System.Threading.CancellationToken)~System.Threading.Tasks.ValueTask")))
        _bits0 = _bits0 | 16384uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1})~System.Collections.Generic.IEnumerable{``0}")))
        _bits0 = _bits0 | 32768uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})~System.Collections.Generic.IEnumerable{``0}")))
        _bits0 = _bits0 | 65536uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})~``0")))
        _bits0 = _bits0 | 131072uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})~``0")))
        _bits0 = _bits0 | 262144uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1})~``0")))
        _bits0 = _bits0 | 524288uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1})~``0")))
        _bits0 = _bits0 | 1048576uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.OrderDescending``1(System.Collections.Generic.IEnumerable{``0})~System.Linq.IOrderedEnumerable{``0}")))
        _bits0 = _bits0 | 2097152uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.OrderDescending``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0})~System.Linq.IOrderedEnumerable{``0}")))
        _bits0 = _bits0 | 4194304uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Order``1(System.Collections.Generic.IEnumerable{``0})~System.Linq.IOrderedEnumerable{``0}")))
        _bits0 = _bits0 | 8388608uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Order``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0})~System.Linq.IOrderedEnumerable{``0}")))
        _bits0 = _bits0 | 16777216uL;
    if (IncludeMember(compilation, options, "M:System.Linq.Enumerable.Zip``2(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Zip``2(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1})~System.Collections.Generic.IEnumerable{System.ValueTuple{``0,``1}}")))
        _bits0 = _bits0 | 33554432uL;
    if (_hasReadOnlySpanOfT && IncludeMember(compilation, options, "M:System.MemoryExtensions.AsSpan(System.String,System.Int32,System.Int32)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.AsSpan(System.String,System.Int32,System.Int32)~System.ReadOnlySpan{System.Char}")))
        _bits0 = _bits0 | 67108864uL;
    if (_hasReadOnlySpanOfT && IncludeMember(compilation, options, "M:System.MemoryExtensions.Contains``1(System.ReadOnlySpan{``0},``0)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Contains``1(System.ReadOnlySpan{``0},``0)~System.Boolean")))
        _bits0 = _bits0 | 134217728uL;
    if (_hasSpanOfT && IncludeMember(compilation, options, "M:System.MemoryExtensions.Contains``1(System.Span{``0},``0)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Contains``1(System.Span{``0},``0)~System.Boolean")))
        _bits0 = _bits0 | 268435456uL;
    if (IncludeMember(compilation, options, "M:System.String.Contains(System.Char)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Contains(System.String,System.Char)~System.Boolean")))
        _bits0 = _bits0 | 536870912uL;
    if (IncludeMember(compilation, options, "M:System.String.Contains(System.Char,System.StringComparison)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Contains(System.String,System.Char,System.StringComparison)~System.Boolean")))
        _bits0 = _bits0 | 1073741824uL;
    if (IncludeMember(compilation, options, "M:System.String.Contains(System.String,System.StringComparison)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Contains(System.String,System.String,System.StringComparison)~System.Boolean")))
        _bits0 = _bits0 | 2147483648uL;
    if (_hasSpanOfT && IncludeMember(compilation, options, "M:System.String.CopyTo(System.Span{System.Char})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.CopyTo(System.String,System.Span{System.Char})")))
        _bits0 = _bits0 | 4294967296uL;
    if (IncludeMember(compilation, options, "M:System.String.EndsWith(System.Char)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.EndsWith(System.String,System.Char)~System.Boolean")))
        _bits0 = _bits0 | 8589934592uL;
    if (IncludeMember(compilation, options, "M:System.String.GetHashCode(System.StringComparison)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.GetHashCode(System.String,System.StringComparison)~System.Int32") && IncludeMember(compilation, options, "M:Helpers.FromComparison(System.StringComparison)~System.StringComparer")))
        _bits0 = _bits0 | 17179869184uL;
    if (IncludeMember(compilation, options, "M:System.String.IndexOf(System.Char,System.StringComparison)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.IndexOf(System.String,System.Char,System.StringComparison)~System.Int32")))
        _bits0 = _bits0 | 34359738368uL;
    if (IncludeMember(compilation, options, "M:System.String.Replace(System.String,System.String,System.StringComparison)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Replace(System.String,System.String,System.String,System.StringComparison)~System.String")))
        _bits0 = _bits0 | 68719476736uL;
    if (IncludeMember(compilation, options, "M:System.String.ReplaceLineEndings(System.String)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReplaceLineEndings(System.String,System.String)~System.String")))
        _bits0 = _bits0 | 137438953472uL;
    if (IncludeMember(compilation, options, "M:System.String.ReplaceLineEndings") && (IncludeMember(compilation, options, "M:PolyfillExtensions.ReplaceLineEndings(System.String)~System.String")))
        _bits0 = _bits0 | 274877906944uL;
    if (IncludeMember(compilation, options, "M:System.String.Split(System.Char,System.Int32,System.StringSplitOptions)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Split(System.String,System.Char,System.Int32,System.StringSplitOptions)~System.String[]")))
        _bits0 = _bits0 | 549755813888uL;
    if (IncludeMember(compilation, options, "M:System.String.Split(System.Char,System.StringSplitOptions)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Split(System.String,System.Char,System.StringSplitOptions)~System.String[]")))
        _bits0 = _bits0 | 1099511627776uL;
    if (IncludeMember(compilation, options, "M:System.String.StartsWith(System.Char)") && (IncludeMember(compilation, options, "M:PolyfillExtensions.StartsWith(System.String,System.Char)~System.Boolean")))
        _bits0 = _bits0 | 2199023255552uL;
    if (_hasSpanOfT && IncludeMember(compilation, options, "M:System.String.TryCopyTo(System.Span{System.Char})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.TryCopyTo(System.String,System.Span{System.Char})~System.Boolean")))
        _bits0 = _bits0 | 4398046511104uL;
    if (_hasReadOnlyMemoryOfT && IncludeMember(compilation, options, "M:System.Text.StringBuilder.Append(System.ReadOnlyMemory{System.Char})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Append(System.Text.StringBuilder,System.ReadOnlyMemory{System.Char})~System.Text.StringBuilder")))
        _bits0 = _bits0 | 8796093022208uL;
    if (_hasReadOnlySpanOfT && IncludeMember(compilation, options, "M:System.Text.StringBuilder.Append(System.ReadOnlySpan{System.Char})") && (IncludeMember(compilation, options, "M:PolyfillExtensions.Append(System.Text.StringBuilder,System.ReadOnlySpan{System.Char})~System.Text.StringBuilder")))
        _bits0 = _bits0 | 17592186044416uL;
    if (IncludeMember(compilation, options, "M:System.Threading.CancellationTokenSource.CancelAsync") && (IncludeMember(compilation, options, "M:PolyfillExtensions.CancelAsync(System.Threading.CancellationTokenSource)~System.Threading.Tasks.Task")))
        _bits0 = _bits0 | 35184372088832uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.AllowNullAttribute"))
        _bits0 = _bits0 | 70368744177664uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DisallowNullAttribute"))
        _bits0 = _bits0 | 140737488355328uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute"))
        _bits0 = _bits0 | 281474976710656uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute"))
        _bits0 = _bits0 | 562949953421312uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"))
        _bits0 = _bits0 | 1125899906842624uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"))
        _bits0 = _bits0 | 2251799813685248uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute"))
        _bits0 = _bits0 | 4503599627370496uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MaybeNullAttribute"))
        _bits0 = _bits0 | 9007199254740992uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute"))
        _bits0 = _bits0 | 18014398509481984uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MemberNotNullAttribute"))
        _bits0 = _bits0 | 36028797018963968uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute"))
        _bits0 = _bits0 | 72057594037927936uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullAttribute"))
        _bits0 = _bits0 | 144115188075855872uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute"))
        _bits0 = _bits0 | 288230376151711744uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.NotNullWhenAttribute"))
        _bits0 = _bits0 | 576460752303423488uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresAssemblyFilesAttribute"))
        _bits0 = _bits0 | 1152921504606846976uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute"))
        _bits0 = _bits0 | 2305843009213693952uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute"))
        _bits0 = _bits0 | 4611686018427387904uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute"))
        _bits0 = _bits0 | 9223372036854775808uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.StringSyntaxAttribute"))
        _bits1 = _bits1 | 1uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"))
        _bits1 = _bits1 | 2uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.CodeAnalysis.UnscopedRefAttribute"))
        _bits1 = _bits1 | 4uL;
    if (IncludeMember(compilation, options, "T:System.Diagnostics.StackTraceHiddenAttribute"))
        _bits1 = _bits1 | 8uL;
    if (IncludeMember(compilation, options, "T:System.HashCode"))
        _bits1 = _bits1 | 16uL;
    if (IncludeMember(compilation, options, "T:System.Index"))
        _bits1 = _bits1 | 64uL;
    if (IncludeMember(compilation, options, "T:System.Range"))
        _bits1 = _bits1 | 128uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.AsyncMethodBuilderAttribute"))
        _bits1 = _bits1 | 256uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"))
        _bits1 = _bits1 | 512uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute"))
        _bits1 = _bits1 | 1024uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute"))
        _bits1 = _bits1 | 2048uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute"))
        _bits1 = _bits1 | 4096uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute"))
        _bits1 = _bits1 | 8192uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.IsExternalInit"))
        _bits1 = _bits1 | 16384uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.ModuleInitializerAttribute"))
        _bits1 = _bits1 | 32768uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.RequiredMemberAttribute"))
        _bits1 = _bits1 | 65536uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.SkipLocalsInitAttribute"))
        _bits1 = _bits1 | 131072uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.CompilerServices.TupleElementNamesAttribute"))
        _bits1 = _bits1 | 262144uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.InteropServices.SuppressGCTransitionAttribute"))
        _bits1 = _bits1 | 524288uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute"))
        _bits1 = _bits1 | 1048576uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.ObsoletedOSPlatformAttribute"))
        _bits1 = _bits1 | 2097152uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.RequiresPreviewFeaturesAttribute"))
        _bits1 = _bits1 | 4194304uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.SupportedOSPlatformAttribute"))
        _bits1 = _bits1 | 8388608uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.SupportedOSPlatformGuardAttribute"))
        _bits1 = _bits1 | 16777216uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.TargetPlatformAttribute"))
        _bits1 = _bits1 | 33554432uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.UnsupportedOSPlatformAttribute"))
        _bits1 = _bits1 | 67108864uL;
    if (IncludeMember(compilation, options, "T:System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute"))
        _bits1 = _bits1 | 134217728uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple"))
        _bits1 = _bits1 | 268435456uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`1"))
        _bits1 = _bits1 | 536870912uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`2"))
        _bits1 = _bits1 | 1073741824uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`3"))
        _bits1 = _bits1 | 2147483648uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`4"))
        _bits1 = _bits1 | 4294967296uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`5"))
        _bits1 = _bits1 | 8589934592uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`6"))
        _bits1 = _bits1 | 17179869184uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`7"))
        _bits1 = _bits1 | 34359738368uL;
    if (IncludeMember(compilation, options, "T:System.ValueTuple`8"))
        _bits1 = _bits1 | 68719476736uL;
    if (((_bits1 & 268435456ul) == 268435456ul || (_bits1 & 536870912ul) == 536870912ul || (_bits1 & 1073741824ul) == 1073741824ul || (_bits1 & 2147483648ul) == 2147483648ul || (_bits1 & 4294967296ul) == 4294967296ul || (_bits1 & 8589934592ul) == 8589934592ul || (_bits1 & 17179869184ul) == 17179869184ul || (_bits1 & 34359738368ul) == 34359738368ul || (_bits1 & 68719476736ul) == 68719476736ul) && IncludeMember(compilation, options, "T:System.ITupleInternal"))
        _bits1 = _bits1 | 32uL;
}
public override int GetHashCode()
{
    var hash = _bits0.GetHashCode();
    hash = hash * 23 + _bits1.GetHashCode();
    return hash;
}
public override bool Equals(object? obj) => obj is Members other && Equals(other);
public bool Equals(Members other) => _bits0 == other._bits0  && _bits1 == other._bits1;
public void AddSources(SourceProductionContext context)
{
    if ((_bits0 & 1ul) == 1ul)
        context.AddSource("M_System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd``1(`0,System.Func{`0,``0,`1},``0).g.cs", PolyfillContents.Source_M_System_Collections_Concurrent_ConcurrentDictionary_2_GetOrAdd__1__0_System_Func__0___0__1____0_);
    if ((_bits0 & 2ul) == 2ul)
        context.AddSource("M_System.Collections.Generic.KeyValuePair`2.Deconstruct(`0_,`1_).g.cs", PolyfillContents.Source_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__);
    if ((_bits0 & 4ul) == 4ul)
        context.AddSource("M_System.Collections.Generic.Queue`1.TryDequeue(`0_).g.cs", PolyfillContents.Source_M_System_Collections_Generic_Queue_1_TryDequeue__0__);
    if ((_bits0 & 8ul) == 8ul)
        context.AddSource("M_System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Int32,System.Int32).g.cs", PolyfillContents.Source_M_System_Collections_Immutable_ImmutableArray_1_AsSpan_System_Int32_System_Int32_);
    if ((_bits0 & 16ul) == 16ul)
        context.AddSource("M_System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Range).g.cs", PolyfillContents.Source_M_System_Collections_Immutable_ImmutableArray_1_AsSpan_System_Range_);
    if ((_bits0 & 32ul) == 32ul)
        context.AddSource("M_System.Diagnostics.Process.WaitForExitAsync(System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_Diagnostics_Process_WaitForExitAsync_System_Threading_CancellationToken_);
    if ((_bits0 & 64ul) == 64ul)
        context.AddSource("M_System.IO.Stream.Read(System.Span{System.Byte}).g.cs", PolyfillContents.Source_M_System_IO_Stream_Read_System_Span_System_Byte__);
    if ((_bits0 & 128ul) == 128ul)
        context.AddSource("M_System.IO.Stream.ReadAsync(System.Memory{System.Byte},System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_Stream_ReadAsync_System_Memory_System_Byte__System_Threading_CancellationToken_);
    if ((_bits0 & 256ul) == 256ul)
        context.AddSource("M_System.IO.Stream.ReadAtLeast(System.Span{System.Byte},System.Int32,System.Boolean).g.cs", PolyfillContents.Source_M_System_IO_Stream_ReadAtLeast_System_Span_System_Byte__System_Int32_System_Boolean_);
    if ((_bits0 & 512ul) == 512ul)
        context.AddSource("M_System.IO.Stream.ReadAtLeastAsync(System.Memory{System.Byte},System.Int32,System.Boolean,System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_Stream_ReadAtLeastAsync_System_Memory_System_Byte__System_Int32_System_Boolean_System_Threading_CancellationToken_);
    if ((_bits0 & 1024ul) == 1024ul)
        context.AddSource("M_System.IO.StreamReader.ReadLineAsync().g.cs", PolyfillContents.Source_M_System_IO_StreamReader_ReadLineAsync__);
    if ((_bits0 & 2048ul) == 2048ul)
        context.AddSource("M_System.IO.StreamReader.ReadLineAsync(System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_StreamReader_ReadLineAsync_System_Threading_CancellationToken_);
    if ((_bits0 & 4096ul) == 4096ul)
        context.AddSource("M_System.IO.TextReader.ReadAsync(System.Memory{System.Char},System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_TextReader_ReadAsync_System_Memory_System_Char__System_Threading_CancellationToken_);
    if ((_bits0 & 8192ul) == 8192ul)
        context.AddSource("M_System.IO.TextReader.ReadToEndAsync(System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_TextReader_ReadToEndAsync_System_Threading_CancellationToken_);
    if ((_bits0 & 16384ul) == 16384ul)
        context.AddSource("M_System.IO.TextWriter.WriteAsync(System.ReadOnlyMemory{System.Char},System.Threading.CancellationToken).g.cs", PolyfillContents.Source_M_System_IO_TextWriter_WriteAsync_System_ReadOnlyMemory_System_Char__System_Threading_CancellationToken_);
    if ((_bits0 & 32768ul) == 32768ul)
        context.AddSource("M_System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_DistinctBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__);
    if ((_bits0 & 65536ul) == 65536ul)
        context.AddSource("M_System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_DistinctBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IEqualityComparer___1__);
    if ((_bits0 & 131072ul) == 131072ul)
        context.AddSource("M_System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_MaxBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__);
    if ((_bits0 & 262144ul) == 262144ul)
        context.AddSource("M_System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_MaxBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IComparer___1__);
    if ((_bits0 & 524288ul) == 524288ul)
        context.AddSource("M_System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_MinBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__);
    if ((_bits0 & 1048576ul) == 1048576ul)
        context.AddSource("M_System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_MinBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IComparer___1__);
    if ((_bits0 & 2097152ul) == 2097152ul)
        context.AddSource("M_System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_OrderDescending__1_System_Collections_Generic_IEnumerable___0__);
    if ((_bits0 & 4194304ul) == 4194304ul)
        context.AddSource("M_System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_OrderDescending__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IComparer___0__);
    if ((_bits0 & 8388608ul) == 8388608ul)
        context.AddSource("M_System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_Order__1_System_Collections_Generic_IEnumerable___0__);
    if ((_bits0 & 16777216ul) == 16777216ul)
        context.AddSource("M_System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_Order__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IComparer___0__);
    if ((_bits0 & 33554432ul) == 33554432ul)
        context.AddSource("M_System.Linq.Enumerable.Zip``2(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1}).g.cs", PolyfillContents.Source_M_System_Linq_Enumerable_Zip__2_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IEnumerable___1__);
    if ((_bits0 & 67108864ul) == 67108864ul)
        context.AddSource("M_System.MemoryExtensions.AsSpan(System.String,System.Int32,System.Int32).g.cs", PolyfillContents.Source_M_System_MemoryExtensions_AsSpan_System_String_System_Int32_System_Int32_);
    if ((_bits0 & 134217728ul) == 134217728ul)
        context.AddSource("M_System.MemoryExtensions.Contains``1(System.ReadOnlySpan{``0},``0).g.cs", PolyfillContents.Source_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_);
    if ((_bits0 & 268435456ul) == 268435456ul)
        context.AddSource("M_System.MemoryExtensions.Contains``1(System.Span{``0},``0).g.cs", PolyfillContents.Source_M_System_MemoryExtensions_Contains__1_System_Span___0____0_);
    if ((_bits0 & 536870912ul) == 536870912ul)
        context.AddSource("M_System.String.Contains(System.Char).g.cs", PolyfillContents.Source_M_System_String_Contains_System_Char_);
    if ((_bits0 & 1073741824ul) == 1073741824ul)
        context.AddSource("M_System.String.Contains(System.Char,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_Contains_System_Char_System_StringComparison_);
    if ((_bits0 & 2147483648ul) == 2147483648ul)
        context.AddSource("M_System.String.Contains(System.String,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_Contains_System_String_System_StringComparison_);
    if ((_bits0 & 4294967296ul) == 4294967296ul)
        context.AddSource("M_System.String.CopyTo(System.Span{System.Char}).g.cs", PolyfillContents.Source_M_System_String_CopyTo_System_Span_System_Char__);
    if ((_bits0 & 8589934592ul) == 8589934592ul)
        context.AddSource("M_System.String.EndsWith(System.Char).g.cs", PolyfillContents.Source_M_System_String_EndsWith_System_Char_);
    if ((_bits0 & 17179869184ul) == 17179869184ul)
        context.AddSource("M_System.String.GetHashCode(System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_GetHashCode_System_StringComparison_);
    if ((_bits0 & 34359738368ul) == 34359738368ul)
        context.AddSource("M_System.String.IndexOf(System.Char,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_IndexOf_System_Char_System_StringComparison_);
    if ((_bits0 & 68719476736ul) == 68719476736ul)
        context.AddSource("M_System.String.Replace(System.String,System.String,System.StringComparison).g.cs", PolyfillContents.Source_M_System_String_Replace_System_String_System_String_System_StringComparison_);
    if ((_bits0 & 137438953472ul) == 137438953472ul)
        context.AddSource("M_System.String.ReplaceLineEndings(System.String).g.cs", PolyfillContents.Source_M_System_String_ReplaceLineEndings_System_String_);
    if ((_bits0 & 274877906944ul) == 274877906944ul)
        context.AddSource("M_System.String.ReplaceLineEndings.g.cs", PolyfillContents.Source_M_System_String_ReplaceLineEndings);
    if ((_bits0 & 549755813888ul) == 549755813888ul)
        context.AddSource("M_System.String.Split(System.Char,System.Int32,System.StringSplitOptions).g.cs", PolyfillContents.Source_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_);
    if ((_bits0 & 1099511627776ul) == 1099511627776ul)
        context.AddSource("M_System.String.Split(System.Char,System.StringSplitOptions).g.cs", PolyfillContents.Source_M_System_String_Split_System_Char_System_StringSplitOptions_);
    if ((_bits0 & 2199023255552ul) == 2199023255552ul)
        context.AddSource("M_System.String.StartsWith(System.Char).g.cs", PolyfillContents.Source_M_System_String_StartsWith_System_Char_);
    if ((_bits0 & 4398046511104ul) == 4398046511104ul)
        context.AddSource("M_System.String.TryCopyTo(System.Span{System.Char}).g.cs", PolyfillContents.Source_M_System_String_TryCopyTo_System_Span_System_Char__);
    if ((_bits0 & 8796093022208ul) == 8796093022208ul)
        context.AddSource("M_System.Text.StringBuilder.Append(System.ReadOnlyMemory{System.Char}).g.cs", PolyfillContents.Source_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__);
    if ((_bits0 & 17592186044416ul) == 17592186044416ul)
        context.AddSource("M_System.Text.StringBuilder.Append(System.ReadOnlySpan{System.Char}).g.cs", PolyfillContents.Source_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__);
    if ((_bits0 & 35184372088832ul) == 35184372088832ul)
        context.AddSource("M_System.Threading.CancellationTokenSource.CancelAsync.g.cs", PolyfillContents.Source_M_System_Threading_CancellationTokenSource_CancelAsync);
    if ((_bits0 & 70368744177664ul) == 70368744177664ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.AllowNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute);
    if ((_bits0 & 140737488355328ul) == 140737488355328ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DisallowNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute);
    if ((_bits0 & 281474976710656ul) == 281474976710656ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute);
    if ((_bits0 & 562949953421312ul) == 562949953421312ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute);
    if ((_bits0 & 1125899906842624ul) == 1125899906842624ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute);
    if ((_bits0 & 2251799813685248ul) == 2251799813685248ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes);
    if ((_bits0 & 4503599627370496ul) == 4503599627370496ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute);
    if ((_bits0 & 9007199254740992ul) == 9007199254740992ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.MaybeNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute);
    if ((_bits0 & 18014398509481984ul) == 18014398509481984ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute);
    if ((_bits0 & 36028797018963968ul) == 36028797018963968ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.MemberNotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute);
    if ((_bits0 & 72057594037927936ul) == 72057594037927936ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute);
    if ((_bits0 & 144115188075855872ul) == 144115188075855872ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullAttribute);
    if ((_bits0 & 288230376151711744ul) == 288230376151711744ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute);
    if ((_bits0 & 576460752303423488ul) == 576460752303423488ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.NotNullWhenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute);
    if ((_bits0 & 1152921504606846976ul) == 1152921504606846976ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresAssemblyFilesAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute);
    if ((_bits0 & 2305843009213693952ul) == 2305843009213693952ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute);
    if ((_bits0 & 4611686018427387904ul) == 4611686018427387904ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute);
    if ((_bits0 & 9223372036854775808ul) == 9223372036854775808ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute);
    if ((_bits1 & 1ul) == 1ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute);
    if ((_bits1 & 2ul) == 2ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute);
    if ((_bits1 & 4ul) == 4ul)
        context.AddSource("T_System.Diagnostics.CodeAnalysis.UnscopedRefAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute);
    if ((_bits1 & 8ul) == 8ul)
        context.AddSource("T_System.Diagnostics.StackTraceHiddenAttribute.g.cs", PolyfillContents.Source_T_System_Diagnostics_StackTraceHiddenAttribute);
    if ((_bits1 & 16ul) == 16ul)
        context.AddSource("T_System.HashCode.g.cs", PolyfillContents.Source_T_System_HashCode);
    if ((_bits1 & 64ul) == 64ul)
        context.AddSource("T_System.Index.g.cs", PolyfillContents.Source_T_System_Index);
    if ((_bits1 & 128ul) == 128ul)
        context.AddSource("T_System.Range.g.cs", PolyfillContents.Source_T_System_Range);
    if ((_bits1 & 256ul) == 256ul)
        context.AddSource("T_System.Runtime.CompilerServices.AsyncMethodBuilderAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute);
    if ((_bits1 & 512ul) == 512ul)
        context.AddSource("T_System.Runtime.CompilerServices.CallerArgumentExpressionAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute);
    if ((_bits1 & 1024ul) == 1024ul)
        context.AddSource("T_System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute);
    if ((_bits1 & 2048ul) == 2048ul)
        context.AddSource("T_System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute);
    if ((_bits1 & 4096ul) == 4096ul)
        context.AddSource("T_System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute);
    if ((_bits1 & 8192ul) == 8192ul)
        context.AddSource("T_System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute);
    if ((_bits1 & 16384ul) == 16384ul)
        context.AddSource("T_System.Runtime.CompilerServices.IsExternalInit.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_IsExternalInit);
    if ((_bits1 & 32768ul) == 32768ul)
        context.AddSource("T_System.Runtime.CompilerServices.ModuleInitializerAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_ModuleInitializerAttribute);
    if ((_bits1 & 65536ul) == 65536ul)
        context.AddSource("T_System.Runtime.CompilerServices.RequiredMemberAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_RequiredMemberAttribute);
    if ((_bits1 & 131072ul) == 131072ul)
        context.AddSource("T_System.Runtime.CompilerServices.SkipLocalsInitAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute);
    if ((_bits1 & 262144ul) == 262144ul)
        context.AddSource("T_System.Runtime.CompilerServices.TupleElementNamesAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_CompilerServices_TupleElementNamesAttribute);
    if ((_bits1 & 524288ul) == 524288ul)
        context.AddSource("T_System.Runtime.InteropServices.SuppressGCTransitionAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute);
    if ((_bits1 & 1048576ul) == 1048576ul)
        context.AddSource("T_System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute);
    if ((_bits1 & 2097152ul) == 2097152ul)
        context.AddSource("T_System.Runtime.Versioning.ObsoletedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute);
    if ((_bits1 & 4194304ul) == 4194304ul)
        context.AddSource("T_System.Runtime.Versioning.RequiresPreviewFeaturesAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute);
    if ((_bits1 & 8388608ul) == 8388608ul)
        context.AddSource("T_System.Runtime.Versioning.SupportedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_SupportedOSPlatformAttribute);
    if ((_bits1 & 16777216ul) == 16777216ul)
        context.AddSource("T_System.Runtime.Versioning.SupportedOSPlatformGuardAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute);
    if ((_bits1 & 33554432ul) == 33554432ul)
        context.AddSource("T_System.Runtime.Versioning.TargetPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_TargetPlatformAttribute);
    if ((_bits1 & 67108864ul) == 67108864ul)
        context.AddSource("T_System.Runtime.Versioning.UnsupportedOSPlatformAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute);
    if ((_bits1 & 134217728ul) == 134217728ul)
        context.AddSource("T_System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute.g.cs", PolyfillContents.Source_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute);
    if ((_bits1 & 268435456ul) == 268435456ul)
        context.AddSource("T_System.ValueTuple.g.cs", PolyfillContents.Source_T_System_ValueTuple);
    if ((_bits1 & 536870912ul) == 536870912ul)
        context.AddSource("T_System.ValueTuple`1.g.cs", PolyfillContents.Source_T_System_ValueTuple_1);
    if ((_bits1 & 1073741824ul) == 1073741824ul)
        context.AddSource("T_System.ValueTuple`2.g.cs", PolyfillContents.Source_T_System_ValueTuple_2);
    if ((_bits1 & 2147483648ul) == 2147483648ul)
        context.AddSource("T_System.ValueTuple`3.g.cs", PolyfillContents.Source_T_System_ValueTuple_3);
    if ((_bits1 & 4294967296ul) == 4294967296ul)
        context.AddSource("T_System.ValueTuple`4.g.cs", PolyfillContents.Source_T_System_ValueTuple_4);
    if ((_bits1 & 8589934592ul) == 8589934592ul)
        context.AddSource("T_System.ValueTuple`5.g.cs", PolyfillContents.Source_T_System_ValueTuple_5);
    if ((_bits1 & 17179869184ul) == 17179869184ul)
        context.AddSource("T_System.ValueTuple`6.g.cs", PolyfillContents.Source_T_System_ValueTuple_6);
    if ((_bits1 & 34359738368ul) == 34359738368ul)
        context.AddSource("T_System.ValueTuple`7.g.cs", PolyfillContents.Source_T_System_ValueTuple_7);
    if ((_bits1 & 68719476736ul) == 68719476736ul)
        context.AddSource("T_System.ValueTuple`8.g.cs", PolyfillContents.Source_T_System_ValueTuple_8);
    if ((_bits1 & 32ul) == 32ul)
        context.AddSource("T_System.ITupleInternal.g.cs", PolyfillContents.Source_T_System_ITupleInternal);
}
public string DumpAsCSharpComment()
{
    var sb = new StringBuilder();
    sb.AppendLine(_options.DumpAsCSharpComment());
    sb.AppendLine("// HasMemoryOfT: " + _hasMemoryOfT);
    sb.AppendLine("// HasReadOnlyMemoryOfT: " + _hasReadOnlyMemoryOfT);
    sb.AppendLine("// HasReadOnlySpanOfT: " + _hasReadOnlySpanOfT);
    sb.AppendLine("// HasSpanOfT: " + _hasSpanOfT);
    sb.AppendLine("// HasValueTask: " + _hasValueTask);
    sb.AppendLine("// HasValueTaskOfT: " + _hasValueTaskOfT);
    sb.AppendLine("//");
    sb.AppendLine("// M:System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd``1(`0,System.Func{`0,``0,`1},``0): " + ((_bits0 & 1ul) == 1ul));
    sb.AppendLine("// M:System.Collections.Generic.KeyValuePair`2.Deconstruct(`0@,`1@): " + ((_bits0 & 2ul) == 2ul));
    sb.AppendLine("// M:System.Collections.Generic.Queue`1.TryDequeue(`0@): " + ((_bits0 & 4ul) == 4ul));
    sb.AppendLine("// M:System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Int32,System.Int32): " + ((_bits0 & 8ul) == 8ul));
    sb.AppendLine("// M:System.Collections.Immutable.ImmutableArray`1.AsSpan(System.Range): " + ((_bits0 & 16ul) == 16ul));
    sb.AppendLine("// M:System.Diagnostics.Process.WaitForExitAsync(System.Threading.CancellationToken): " + ((_bits0 & 32ul) == 32ul));
    sb.AppendLine("// M:System.IO.Stream.Read(System.Span{System.Byte}): " + ((_bits0 & 64ul) == 64ul));
    sb.AppendLine("// M:System.IO.Stream.ReadAsync(System.Memory{System.Byte},System.Threading.CancellationToken): " + ((_bits0 & 128ul) == 128ul));
    sb.AppendLine("// M:System.IO.Stream.ReadAtLeast(System.Span{System.Byte},System.Int32,System.Boolean): " + ((_bits0 & 256ul) == 256ul));
    sb.AppendLine("// M:System.IO.Stream.ReadAtLeastAsync(System.Memory{System.Byte},System.Int32,System.Boolean,System.Threading.CancellationToken): " + ((_bits0 & 512ul) == 512ul));
    sb.AppendLine("// M:System.IO.StreamReader.ReadLineAsync(): " + ((_bits0 & 1024ul) == 1024ul));
    sb.AppendLine("// M:System.IO.StreamReader.ReadLineAsync(System.Threading.CancellationToken): " + ((_bits0 & 2048ul) == 2048ul));
    sb.AppendLine("// M:System.IO.TextReader.ReadAsync(System.Memory{System.Char},System.Threading.CancellationToken): " + ((_bits0 & 4096ul) == 4096ul));
    sb.AppendLine("// M:System.IO.TextReader.ReadToEndAsync(System.Threading.CancellationToken): " + ((_bits0 & 8192ul) == 8192ul));
    sb.AppendLine("// M:System.IO.TextWriter.WriteAsync(System.ReadOnlyMemory{System.Char},System.Threading.CancellationToken): " + ((_bits0 & 16384ul) == 16384ul));
    sb.AppendLine("// M:System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}): " + ((_bits0 & 32768ul) == 32768ul));
    sb.AppendLine("// M:System.Linq.Enumerable.DistinctBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IEqualityComparer{``1}): " + ((_bits0 & 65536ul) == 65536ul));
    sb.AppendLine("// M:System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}): " + ((_bits0 & 131072ul) == 131072ul));
    sb.AppendLine("// M:System.Linq.Enumerable.MaxBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1}): " + ((_bits0 & 262144ul) == 262144ul));
    sb.AppendLine("// M:System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1}): " + ((_bits0 & 524288ul) == 524288ul));
    sb.AppendLine("// M:System.Linq.Enumerable.MinBy``2(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Collections.Generic.IComparer{``1}): " + ((_bits0 & 1048576ul) == 1048576ul));
    sb.AppendLine("// M:System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0}): " + ((_bits0 & 2097152ul) == 2097152ul));
    sb.AppendLine("// M:System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0}): " + ((_bits0 & 4194304ul) == 4194304ul));
    sb.AppendLine("// M:System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0}): " + ((_bits0 & 8388608ul) == 8388608ul));
    sb.AppendLine("// M:System.Linq.Enumerable.Order``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0}): " + ((_bits0 & 16777216ul) == 16777216ul));
    sb.AppendLine("// M:System.Linq.Enumerable.Zip``2(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IEnumerable{``1}): " + ((_bits0 & 33554432ul) == 33554432ul));
    sb.AppendLine("// M:System.MemoryExtensions.AsSpan(System.String,System.Int32,System.Int32): " + ((_bits0 & 67108864ul) == 67108864ul));
    sb.AppendLine("// M:System.MemoryExtensions.Contains``1(System.ReadOnlySpan{``0},``0): " + ((_bits0 & 134217728ul) == 134217728ul));
    sb.AppendLine("// M:System.MemoryExtensions.Contains``1(System.Span{``0},``0): " + ((_bits0 & 268435456ul) == 268435456ul));
    sb.AppendLine("// M:System.String.Contains(System.Char): " + ((_bits0 & 536870912ul) == 536870912ul));
    sb.AppendLine("// M:System.String.Contains(System.Char,System.StringComparison): " + ((_bits0 & 1073741824ul) == 1073741824ul));
    sb.AppendLine("// M:System.String.Contains(System.String,System.StringComparison): " + ((_bits0 & 2147483648ul) == 2147483648ul));
    sb.AppendLine("// M:System.String.CopyTo(System.Span{System.Char}): " + ((_bits0 & 4294967296ul) == 4294967296ul));
    sb.AppendLine("// M:System.String.EndsWith(System.Char): " + ((_bits0 & 8589934592ul) == 8589934592ul));
    sb.AppendLine("// M:System.String.GetHashCode(System.StringComparison): " + ((_bits0 & 17179869184ul) == 17179869184ul));
    sb.AppendLine("// M:System.String.IndexOf(System.Char,System.StringComparison): " + ((_bits0 & 34359738368ul) == 34359738368ul));
    sb.AppendLine("// M:System.String.Replace(System.String,System.String,System.StringComparison): " + ((_bits0 & 68719476736ul) == 68719476736ul));
    sb.AppendLine("// M:System.String.ReplaceLineEndings(System.String): " + ((_bits0 & 137438953472ul) == 137438953472ul));
    sb.AppendLine("// M:System.String.ReplaceLineEndings: " + ((_bits0 & 274877906944ul) == 274877906944ul));
    sb.AppendLine("// M:System.String.Split(System.Char,System.Int32,System.StringSplitOptions): " + ((_bits0 & 549755813888ul) == 549755813888ul));
    sb.AppendLine("// M:System.String.Split(System.Char,System.StringSplitOptions): " + ((_bits0 & 1099511627776ul) == 1099511627776ul));
    sb.AppendLine("// M:System.String.StartsWith(System.Char): " + ((_bits0 & 2199023255552ul) == 2199023255552ul));
    sb.AppendLine("// M:System.String.TryCopyTo(System.Span{System.Char}): " + ((_bits0 & 4398046511104ul) == 4398046511104ul));
    sb.AppendLine("// M:System.Text.StringBuilder.Append(System.ReadOnlyMemory{System.Char}): " + ((_bits0 & 8796093022208ul) == 8796093022208ul));
    sb.AppendLine("// M:System.Text.StringBuilder.Append(System.ReadOnlySpan{System.Char}): " + ((_bits0 & 17592186044416ul) == 17592186044416ul));
    sb.AppendLine("// M:System.Threading.CancellationTokenSource.CancelAsync: " + ((_bits0 & 35184372088832ul) == 35184372088832ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.AllowNullAttribute: " + ((_bits0 & 70368744177664ul) == 70368744177664ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DisallowNullAttribute: " + ((_bits0 & 140737488355328ul) == 140737488355328ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute: " + ((_bits0 & 281474976710656ul) == 281474976710656ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute: " + ((_bits0 & 562949953421312ul) == 562949953421312ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute: " + ((_bits0 & 1125899906842624ul) == 1125899906842624ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes: " + ((_bits0 & 2251799813685248ul) == 2251799813685248ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute: " + ((_bits0 & 4503599627370496ul) == 4503599627370496ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.MaybeNullAttribute: " + ((_bits0 & 9007199254740992ul) == 9007199254740992ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute: " + ((_bits0 & 18014398509481984ul) == 18014398509481984ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.MemberNotNullAttribute: " + ((_bits0 & 36028797018963968ul) == 36028797018963968ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute: " + ((_bits0 & 72057594037927936ul) == 72057594037927936ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.NotNullAttribute: " + ((_bits0 & 144115188075855872ul) == 144115188075855872ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute: " + ((_bits0 & 288230376151711744ul) == 288230376151711744ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.NotNullWhenAttribute: " + ((_bits0 & 576460752303423488ul) == 576460752303423488ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.RequiresAssemblyFilesAttribute: " + ((_bits0 & 1152921504606846976ul) == 1152921504606846976ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute: " + ((_bits0 & 2305843009213693952ul) == 2305843009213693952ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute: " + ((_bits0 & 4611686018427387904ul) == 4611686018427387904ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute: " + ((_bits0 & 9223372036854775808ul) == 9223372036854775808ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.StringSyntaxAttribute: " + ((_bits1 & 1ul) == 1ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute: " + ((_bits1 & 2ul) == 2ul));
    sb.AppendLine("// T:System.Diagnostics.CodeAnalysis.UnscopedRefAttribute: " + ((_bits1 & 4ul) == 4ul));
    sb.AppendLine("// T:System.Diagnostics.StackTraceHiddenAttribute: " + ((_bits1 & 8ul) == 8ul));
    sb.AppendLine("// T:System.HashCode: " + ((_bits1 & 16ul) == 16ul));
    sb.AppendLine("// T:System.Index: " + ((_bits1 & 64ul) == 64ul));
    sb.AppendLine("// T:System.Range: " + ((_bits1 & 128ul) == 128ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.AsyncMethodBuilderAttribute: " + ((_bits1 & 256ul) == 256ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.CallerArgumentExpressionAttribute: " + ((_bits1 & 512ul) == 512ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute: " + ((_bits1 & 1024ul) == 1024ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.DisableRuntimeMarshallingAttribute: " + ((_bits1 & 2048ul) == 2048ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute: " + ((_bits1 & 4096ul) == 4096ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.InterpolatedStringHandlerAttribute: " + ((_bits1 & 8192ul) == 8192ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.IsExternalInit: " + ((_bits1 & 16384ul) == 16384ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.ModuleInitializerAttribute: " + ((_bits1 & 32768ul) == 32768ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.RequiredMemberAttribute: " + ((_bits1 & 65536ul) == 65536ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.SkipLocalsInitAttribute: " + ((_bits1 & 131072ul) == 131072ul));
    sb.AppendLine("// T:System.Runtime.CompilerServices.TupleElementNamesAttribute: " + ((_bits1 & 262144ul) == 262144ul));
    sb.AppendLine("// T:System.Runtime.InteropServices.SuppressGCTransitionAttribute: " + ((_bits1 & 524288ul) == 524288ul));
    sb.AppendLine("// T:System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute: " + ((_bits1 & 1048576ul) == 1048576ul));
    sb.AppendLine("// T:System.Runtime.Versioning.ObsoletedOSPlatformAttribute: " + ((_bits1 & 2097152ul) == 2097152ul));
    sb.AppendLine("// T:System.Runtime.Versioning.RequiresPreviewFeaturesAttribute: " + ((_bits1 & 4194304ul) == 4194304ul));
    sb.AppendLine("// T:System.Runtime.Versioning.SupportedOSPlatformAttribute: " + ((_bits1 & 8388608ul) == 8388608ul));
    sb.AppendLine("// T:System.Runtime.Versioning.SupportedOSPlatformGuardAttribute: " + ((_bits1 & 16777216ul) == 16777216ul));
    sb.AppendLine("// T:System.Runtime.Versioning.TargetPlatformAttribute: " + ((_bits1 & 33554432ul) == 33554432ul));
    sb.AppendLine("// T:System.Runtime.Versioning.UnsupportedOSPlatformAttribute: " + ((_bits1 & 67108864ul) == 67108864ul));
    sb.AppendLine("// T:System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute: " + ((_bits1 & 134217728ul) == 134217728ul));
    sb.AppendLine("// T:System.ValueTuple: " + ((_bits1 & 268435456ul) == 268435456ul));
    sb.AppendLine("// T:System.ValueTuple`1: " + ((_bits1 & 536870912ul) == 536870912ul));
    sb.AppendLine("// T:System.ValueTuple`2: " + ((_bits1 & 1073741824ul) == 1073741824ul));
    sb.AppendLine("// T:System.ValueTuple`3: " + ((_bits1 & 2147483648ul) == 2147483648ul));
    sb.AppendLine("// T:System.ValueTuple`4: " + ((_bits1 & 4294967296ul) == 4294967296ul));
    sb.AppendLine("// T:System.ValueTuple`5: " + ((_bits1 & 8589934592ul) == 8589934592ul));
    sb.AppendLine("// T:System.ValueTuple`6: " + ((_bits1 & 17179869184ul) == 17179869184ul));
    sb.AppendLine("// T:System.ValueTuple`7: " + ((_bits1 & 34359738368ul) == 34359738368ul));
    sb.AppendLine("// T:System.ValueTuple`8: " + ((_bits1 & 68719476736ul) == 68719476736ul));
    sb.AppendLine("// T:System.ITupleInternal: " + ((_bits1 & 32ul) == 32ul));
    return sb.ToString();
}
}
file static class PolyfillContents
{
public static SourceText Source_M_System_Collections_Concurrent_ConcurrentDictionary_2_GetOrAdd__1__0_System_Func__0___0__1____0_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Concurrent;

static partial class PolyfillExtensions
{
    public static TValue GetOrAdd<TKey, TValue, TArg>(this ConcurrentDictionary<TKey, TValue> target, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
        where TKey : notnull
    {
        return target.GetOrAdd(key, key => valueFactory(key, factoryArgument));
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Collections_Generic_KeyValuePair_2_Deconstruct__0___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> target, out TKey key, out TValue value)
    {
        key = target.Key;
        value = target.Value;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Collections_Generic_Queue_1_TryDequeue__0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

static partial class PolyfillExtensions
{
    public static bool TryDequeue<T>(this Queue<T> target, [MaybeNullWhen(false)] out T result)
    {
        if (target.Count == 0)
        {
            result = default;
            return false;
        }

        result = target.Dequeue();
        return true;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Collections_Immutable_ImmutableArray_1_AsSpan_System_Int32_System_Int32_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Immutable;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<T> AsSpan<T>(this ImmutableArray<T> target, int start, int length)
    {
        return target.AsSpan().Slice(start, length);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Collections_Immutable_ImmutableArray_1_AsSpan_System_Range_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Immutable;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<T> AsSpan<T>(this ImmutableArray<T> target, Range range)
    {
        (int start, int length) = range.GetOffsetAndLength(target.Length);
        return target.AsSpan().Slice(start, length);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Diagnostics_Process_WaitForExitAsync_System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System;

static partial class PolyfillExtensions
{
    public static async Task WaitForExitAsync(this Process target, CancellationToken cancellationToken = default)
    {
        // https://source.dot.net/#System.Diagnostics.Process/System/Diagnostics/Process.cs,b6a5b00714a61f06
        // Because the process has already started by the time this method is called,
        // we're in a race against the process to set up our exit handlers before the process
        // exits. As a result, there are several different flows that must be handled:
        //
        // CASE 1: WE ENABLE EVENTS
        // This is the "happy path". In this case we enable events.
        //
        // CASE 1.1: PROCESS EXITS OR IS CANCELED AFTER REGISTERING HANDLER
        // This case continues the "happy path". The process exits or waiting is canceled after
        // registering the handler and no special cases are needed.
        //
        // CASE 1.2: PROCESS EXITS BEFORE REGISTERING HANDLER
        // It's possible that the process can exit after we enable events but before we reigster
        // the handler. In that case we must check for exit after registering the handler.
        //
        //
        // CASE 2: PROCESS EXITS BEFORE ENABLING EVENTS
        // The process may exit before we attempt to enable events. In that case EnableRaisingEvents
        // will throw an exception like this:
        //     System.InvalidOperationException : Cannot process request because the process (42) has exited.
        // In this case we catch the InvalidOperationException. If the process has exited, our work
        // is done and we return. If for any reason (now or in the future) enabling events fails
        // and the process has not exited, bubble the exception up to the user.
        //
        //
        // CASE 3: USER ALREADY ENABLED EVENTS
        // In this case the user has already enabled raising events. Re-enabling events is a no-op
        // as the value hasn't changed. However, no-op also means that if the process has already
        // exited, EnableRaisingEvents won't throw an exception.
        //
        // CASE 3.1: PROCESS EXITS OR IS CANCELED AFTER REGISTERING HANDLER
        // (See CASE 1.1)
        //
        // CASE 3.2: PROCESS EXITS BEFORE REGISTERING HANDLER
        // (See CASE 1.2)
        if (!target.HasExited)
        {
            // Early out for cancellation before doing more expensive work
            cancellationToken.ThrowIfCancellationRequested();
        }
        try
        {
            // CASE 1: We enable events
            // CASE 2: Process exits before enabling events (and throws an exception)
            // CASE 3: User already enabled events (no-op)
            target.EnableRaisingEvents = true;
        }
        catch (InvalidOperationException)
        {
            // CASE 2: If the process has exited, our work is done, otherwise bubble the
            // exception up to the user
            if (target.HasExited)
            {
                return;
            }
            throw;
        }
        var tcs = new TaskCompletionSourceWithCancellation<bool>();
        void Handler(object? s, EventArgs e) => tcs.TrySetResult(true);
        target.Exited += Handler;
        try
        {
            if (target.HasExited)
            {
                // CASE 1.2 & CASE 3.2: Handle race where the process exits before registering the handler
                return;
            }
            // CASE 1.1 & CASE 3.1: Process exits or is canceled here
            await tcs.WaitWithCancellationAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            target.Exited -= Handler;
        }

        target.WaitForExit();
    }

    private sealed class TaskCompletionSourceWithCancellation<T> : TaskCompletionSource<T>
    {
        private CancellationToken _cancellationToken;
        public TaskCompletionSourceWithCancellation() : base(TaskCreationOptions.RunContinuationsAsynchronously)
        {
        }
        private void OnCancellation()
        {
            TrySetCanceled(_cancellationToken);
        }
#if NETCOREAPP3_1_OR_GREATER
        public async ValueTask<T> WaitWithCancellationAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            await using (cancellationToken.UnsafeRegister(s => ((TaskCompletionSourceWithCancellation<T>)s!).OnCancellation(), this))
            {
                return await Task.ConfigureAwait(false);
            }
        }
#else
        public async Task<T> WaitWithCancellationAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            using (cancellationToken.Register(s => ((TaskCompletionSourceWithCancellation<T>)s!).OnCancellation(), this))
            {
                return await Task.ConfigureAwait(false);
            }
        }
#endif
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_Stream_Read_System_Span_System_Byte__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static int Read(this Stream target, Span<byte> buffer)
    {
        var bufferTemp = new byte[buffer.Length];
        var read = target.Read(bufferTemp, 0, bufferTemp.Length);
        bufferTemp.AsSpan(0, read).CopyTo(buffer);
        return read;
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_Stream_ReadAsync_System_Memory_System_Byte__System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async ValueTask<int> ReadAsync(this Stream target, Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<byte>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        var read = await target.ReadAsync(segment.Array!, 0, buffer.Length);
        return read;
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_Stream_ReadAtLeast_System_Span_System_Byte__System_Int32_System_Boolean_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;

static partial class PolyfillExtensions
{
    public static int ReadAtLeast(this Stream target, Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true)
    {
        if (minimumBytes < 0)
             throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Non-negative number required");

        if (buffer.Length < minimumBytes)
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Must not be greater than the length of the buffer.");

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = target.Read(buffer.Slice(totalRead));
            if (read == 0)
            {
                if (throwOnEndOfStream)
                    throw new EndOfStreamException("Unable to read beyond the end of the stream.");

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_Stream_ReadAtLeastAsync_System_Memory_System_Byte__System_Int32_System_Boolean_System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async ValueTask<int> ReadAtLeastAsync(this Stream target, Memory<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true, CancellationToken cancellationToken = default)
    {
        if (minimumBytes < 0)
             throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Non-negative number required");

        if (buffer.Length < minimumBytes)
            throw new ArgumentOutOfRangeException(nameof(minimumBytes), "Must not be greater than the length of the buffer.");

        int totalRead = 0;
        while (totalRead < minimumBytes)
        {
            int read = await target.ReadAsync(buffer.Slice(totalRead), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                if (throwOnEndOfStream)
                {
                    throw new EndOfStreamException("Unable to read beyond the end of the stream.");
                }

                return totalRead;
            }

            totalRead += read;
        }

        return totalRead;
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_StreamReader_ReadLineAsync__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task<string?> ReadLineAsync(this StreamReader target)
    {
        return Task.FromResult(target.ReadLine());
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_StreamReader_ReadLineAsync_System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static ValueTask<string?> ReadLineAsync(this StreamReader target, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return new(target.ReadLineAsync());
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_TextReader_ReadAsync_System_Memory_System_Char__System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

static partial class PolyfillExtensions
{
    public static ValueTask<int> ReadAsync(this TextReader target, Memory<char> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray((ReadOnlyMemory<char>)buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.ReadAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_TextReader_ReadToEndAsync_System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task<string> ReadToEndAsync(this TextReader target, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return target.ReadToEndAsync();
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_IO_TextWriter_WriteAsync_System_ReadOnlyMemory_System_Char__System_Threading_CancellationToken_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

static partial class PolyfillExtensions
{
    public static ValueTask WriteAsync(this TextWriter target, ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!MemoryMarshal.TryGetArray(buffer, out var segment))
        {
            segment = new(buffer.ToArray());
        }

        return new(target.WriteAsync(segment.Array!, segment.Offset, segment.Count));
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_DistinctBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
    {
        var hashSet = new HashSet<TKey>(comparer);
        foreach (var item in source)
        {
            var key = keySelector(item);
            if (hashSet.Add(key))
                yield return item;
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_DistinctBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IEqualityComparer___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var hashSet = new HashSet<TKey>();
        foreach (var item in source)
        {
            var key = keySelector(item);
            if (hashSet.Add(key))
                yield return item;
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_MaxBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return source.MaxBy(keySelector, comparer: null);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_MaxBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IComparer___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TSource? MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (keySelector == null)
            throw new ArgumentNullException(nameof(keySelector));

        comparer ??= Comparer<TKey>.Default;

        using IEnumerator<TSource> e = source.GetEnumerator();

        if (!e.MoveNext())
        {
            if (default(TSource) is null)
            {
                return default;
            }
            else
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }

        TSource value = e.Current;
        TKey key = keySelector(value);

        if (default(TKey) is null)
        {
            if (key == null)
            {
                TSource firstValue = value;

                do
                {
                    if (!e.MoveNext())
                    {
                        // All keys are null, surface the first element.
                        return firstValue;
                    }

                    value = e.Current;
                    key = keySelector(value);
                }
                while (key == null);
            }

            while (e.MoveNext())
            {
                TSource nextValue = e.Current;
                TKey nextKey = keySelector(nextValue);
                if (nextKey != null && comparer.Compare(nextKey, key) > 0)
                {
                    key = nextKey;
                    value = nextValue;
                }
            }
        }
        else
        {
            if (comparer == Comparer<TKey>.Default)
            {
                while (e.MoveNext())
                {
                    TSource nextValue = e.Current;
                    TKey nextKey = keySelector(nextValue);
                    if (Comparer<TKey>.Default.Compare(nextKey, key) > 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
            else
            {
                while (e.MoveNext())
                {
                    TSource nextValue = e.Current;
                    TKey nextKey = keySelector(nextValue);
                    if (comparer.Compare(nextKey, key) > 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
        }

        return value;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_MinBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return source.MinBy(keySelector, comparer: null);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_MinBy__2_System_Collections_Generic_IEnumerable___0__System_Func___0___1__System_Collections_Generic_IComparer___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Collections.Generic;

static partial class PolyfillExtensions
{
    public static TSource? MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (keySelector == null)
            throw new ArgumentNullException(nameof(keySelector));

        comparer ??= Comparer<TKey>.Default;

        using IEnumerator<TSource> e = source.GetEnumerator();

        if (!e.MoveNext())
        {
            if (default(TSource) is null)
            {
                return default;
            }
            else
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }

        TSource value = e.Current;
        TKey key = keySelector(value);

        if (default(TKey) is null)
        {
            if (key == null)
            {
                TSource firstValue = value;

                do
                {
                    if (!e.MoveNext())
                    {
                        // All keys are null, surface the first element.
                        return firstValue;
                    }

                    value = e.Current;
                    key = keySelector(value);
                }
                while (key == null);
            }

            while (e.MoveNext())
            {
                TSource nextValue = e.Current;
                TKey nextKey = keySelector(nextValue);
                if (nextKey != null && comparer.Compare(nextKey, key) < 0)
                {
                    key = nextKey;
                    value = nextValue;
                }
            }
        }
        else
        {
            if (comparer == Comparer<TKey>.Default)
            {
                while (e.MoveNext())
                {
                    TSource nextValue = e.Current;
                    TKey nextKey = keySelector(nextValue);
                    if (Comparer<TKey>.Default.Compare(nextKey, key) < 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
            else
            {
                while (e.MoveNext())
                {
                    TSource nextValue = e.Current;
                    TKey nextKey = keySelector(nextValue);
                    if (comparer.Compare(nextKey, key) < 0)
                    {
                        key = nextKey;
                        value = nextValue;
                    }
                }
            }
        }

        return value;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_OrderDescending__1_System_Collections_Generic_IEnumerable___0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source)
    {
        return source.OrderByDescending(_ => _);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_OrderDescending__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IComparer___0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> OrderDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
    {
        return source.OrderByDescending(_ => _, comparer);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_Order__1_System_Collections_Generic_IEnumerable___0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(_ => _);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_Order__1_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IComparer___0__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IOrderedEnumerable<T> Order<T>(this IEnumerable<T> source, IComparer<T>? comparer)
    {
        return source.OrderBy(_ => _, comparer);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Linq_Enumerable_Zip__2_System_Collections_Generic_IEnumerable___0__System_Collections_Generic_IEnumerable___1__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections.Generic;
using System.Linq;

static partial class PolyfillExtensions
{
    public static IEnumerable<(TFirst left, TSecond right)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
    {
        return first.Zip(second, (x, y) => (x, y));
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_MemoryExtensions_AsSpan_System_String_System_Int32_System_Int32_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static ReadOnlySpan<char> AsSpan(this string? text, int start, int length)
    {
        if (text == null)
        {
            if (start != 0 || length != 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            return default;
        }

        return text.AsSpan().Slice(start, length);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_MemoryExtensions_Contains__1_System_ReadOnlySpan___0____0_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
    {
        if (default(T) != null || value is not null)
        {
            foreach (var item in span)
            {
                if (value!.Equals(item))
                    return true;
            }
        }
        else
        {
            foreach (var item in span)
            {
                if (item is null)
                    return true;
            }
        }

        return false;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_MemoryExtensions_Contains__1_System_Span___0____0_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static bool Contains<T>(this Span<T> span, T value) where T : IEquatable<T>?
    {
        if (default(T) != null || value is not null)
        {
            foreach (var item in span)
            {
                if (value!.Equals(item))
                    return true;
            }
        }
        else
        {
            foreach (var item in span)
            {
                if (item is null)
                    return true;
            }
        }

        return false;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Contains_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
	public static bool Contains(this string target, char value)
	{
		return target.IndexOf(value) != -1;
	}
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Contains_System_Char_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
	public static bool Contains(this string target, char value, System.StringComparison comparisonType)
	{
		return target.IndexOf(value, comparisonType) != -1;
	}
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Contains_System_String_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
	public static bool Contains(this string target, string value, System.StringComparison comparisonType)
	{
		return target.IndexOf(value, comparisonType) != -1;
	}
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_CopyTo_System_Span_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static void CopyTo(this string target, Span<char> destination)
    {
        target.AsSpan().CopyTo(destination);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_EndsWith_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
    public static bool EndsWith(this string target, char value)
    {
        return target.Length > 0 && target[target.Length - 1] == value;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_GetHashCode_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static int GetHashCode(this string target, StringComparison comparisonType)
    {
        return Helpers.FromComparison(comparisonType).GetHashCode(target);
    }
}

file class Helpers
{
    public static StringComparer FromComparison(StringComparison comparisonType) =>
        comparisonType switch
        {
            StringComparison.CurrentCulture => StringComparer.CurrentCulture,
            StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
            StringComparison.InvariantCulture => StringComparer.InvariantCulture,
            StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
            StringComparison.Ordinal => StringComparer.Ordinal,
            StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
            _ => throw new ArgumentException("The string comparison type passed in is currently not supported.", nameof(comparisonType)),
        };
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_IndexOf_System_Char_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
    public static int IndexOf(this string target, char value, System.StringComparison comparisonType)
    {
        return target.IndexOf(value.ToString(), comparisonType);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Replace_System_String_System_String_System_StringComparison_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static string Replace(this string target, string oldValue, string? newValue, StringComparison comparisonType)
    {
        if (oldValue == null)
            throw new ArgumentNullException(nameof(oldValue));

        if (oldValue == "")
            throw new ArgumentException("The value cannot be an empty string.", nameof(oldValue));

        var sb = new StringBuilder();

        var previousIndex = 0;
        while (target.IndexOf(oldValue, previousIndex, comparisonType) is var index and not -1)
        {
            sb.Append(target, previousIndex, index - previousIndex);
            sb.Append(newValue);
            previousIndex = index + oldValue.Length;
        }

        sb.Append(target, previousIndex, target.Length - previousIndex);
        return sb.ToString();
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_ReplaceLineEndings_System_String_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Text;

static partial class PolyfillExtensions
{
    public static string ReplaceLineEndings(this string target, string replacementText)
    {
        var sb = new StringBuilder();

        var previousIndex = 0;
        while (target.IndexOfAny(Constants.NewLineChars, previousIndex) is var index and not -1)
        {
            sb.Append(target, previousIndex, index - previousIndex);
            sb.Append(replacementText);

            previousIndex = index + 1;
            if (target[index] == '\r' && index + 1 < target.Length && target[index + 1] == '\n')
            {
                previousIndex++;
            }
        }

        sb.Append(target, previousIndex, target.Length - previousIndex);
        return sb.ToString();
    }
}

file static class Constants
{
    public static readonly char[] NewLineChars = new char[] { '\n', '\r', '\f', '\u0085', '\u2028', '\u2029' };
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_ReplaceLineEndings { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
    public static string ReplaceLineEndings(this string target)
    {
        return target.ReplaceLineEndings(System.Environment.NewLine);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Split_System_Char_System_Int32_System_StringSplitOptions_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static string[] Split(this string target, char separator, int count, StringSplitOptions options = StringSplitOptions.None)
    {
        return target.Split(new char[] { separator }, count, options);
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_Split_System_Char_System_StringSplitOptions_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static string[] Split(this string target, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        return target.Split(new char[] { separator }, options);
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_StartsWith_System_Char_ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
static partial class PolyfillExtensions
{
    public static bool StartsWith(this string target, char value)
    {
        return target.Length > 0 && target[0] == value;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_String_TryCopyTo_System_Span_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;

static partial class PolyfillExtensions
{
    public static bool TryCopyTo(this string target, Span<char> destination)
    {
        return target.AsSpan().TryCopyTo(destination);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Text_StringBuilder_Append_System_ReadOnlyMemory_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlyMemory<char> value)
    {
        if (value.IsEmpty)
            return target;

        return target.Append(value.ToArray());
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Text_StringBuilder_Append_System_ReadOnlySpan_System_Char__ { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
            return target;

        return target.Append(value.ToArray());
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_M_System_Threading_CancellationTokenSource_CancelAsync { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static Task CancelAsync(this CancellationTokenSource target)
    {
        target.Cancel();
        return Task.CompletedTask;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_AllowNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///   Specifies that <see langword="null"/> is allowed as an input even if the
///   corresponding type disallows it.
/// </summary>
/// <summary>Specifies that null is allowed as an input even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal sealed class AllowNullAttribute : Attribute
{
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DisallowNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that null is disallowed as an input even if the corresponding type allows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal sealed class DisallowNullAttribute : Attribute
{ }
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Applied to a method that will never return under any circumstance.</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal sealed class DoesNotReturnAttribute : Attribute
{
}

"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DoesNotReturnIfAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method will not return if the associated Boolean parameter is passed the specified value.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class DoesNotReturnIfAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified parameter value.</summary>
    /// <param name="parameterValue">
    /// The condition parameter value. Code after the method will be considered unreachable by diagnostics if the argument to
    /// the associated parameter matches this value.
    /// </param>
    public DoesNotReturnIfAttribute(bool parameterValue) => ParameterValue = parameterValue;

    /// <summary>Gets the condition parameter value.</summary>
    public bool ParameterValue { get; }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicDependencyAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// States a dependency that one member has on another.
    /// </summary>
    /// <remarks>
    /// This can be used to inform tooling of a dependency that is otherwise not evident purely from
    /// metadata and IL, for example a member relied on via reflection.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method,
        AllowMultiple = true, Inherited = false)]
    internal sealed class DynamicDependencyAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on the same type as the consumer.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        public DynamicDependencyAttribute(string memberSignature)
        {
            MemberSignature = memberSignature;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on a <see cref="global::System.Type"/>.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        /// <param name="type">The <see cref="global::System.Type"/> containing <paramref name="memberSignature"/>.</param>
        public DynamicDependencyAttribute(string memberSignature, global::System.Type type)
        {
            MemberSignature = memberSignature;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicDependencyAttribute"/> class
        /// with the specified signature of a member on a type in an assembly.
        /// </summary>
        /// <param name="memberSignature">The signature of the member depended on.</param>
        /// <param name="typeName">The full name of the type containing the specified member.</param>
        /// <param name="assemblyName">The assembly name of the type containing the specified member.</param>
        public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
        {
            MemberSignature = memberSignature;
            TypeName = typeName;
            AssemblyName = assemblyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified types of members on a <see cref="global::System.Type"/>.
        /// </summary>
        /// <param name="memberTypes">The types of members depended on.</param>
        /// <param name="type">The <see cref="global::System.Type"/> containing the specified members.</param>
        public DynamicDependencyAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes, global::System.Type type)
        {
            MemberTypes = memberTypes;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicDependencyAttribute"/> class
        /// with the specified types of members on a type in an assembly.
        /// </summary>
        /// <param name="memberTypes">The types of members depended on.</param>
        /// <param name="typeName">The full name of the type containing the specified members.</param>
        /// <param name="assemblyName">The assembly name of the type containing the specified members.</param>
        public DynamicDependencyAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
        {
            MemberTypes = memberTypes;
            TypeName = typeName;
            AssemblyName = assemblyName;
        }

        /// <summary>
        /// Gets the signature of the member depended on.
        /// </summary>
        /// <remarks>
        /// Either <see cref="MemberSignature"/> must be a valid string or <see cref="MemberTypes"/>
        /// must not equal <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None"/>, but not both.
        /// </remarks>
        public string? MemberSignature { get; }

        /// <summary>
        /// Gets the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"/> which specifies the type
        /// of members depended on.
        /// </summary>
        /// <remarks>
        /// Either <see cref="MemberSignature"/> must be a valid string or <see cref="MemberTypes"/>
        /// must not equal <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None"/>, but not both.
        /// </remarks>
        public global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MemberTypes { get; }

        /// <summary>
        /// Gets the <see cref="global::System.Type"/> containing the specified member.
        /// </summary>
        /// <remarks>
        /// If neither <see cref="Type"/> nor <see cref="TypeName"/> are specified,
        /// the type of the consumer is assumed.
        /// </remarks>
        public global::System.Type? Type { get; }

        /// <summary>
        /// Gets the full name of the type containing the specified member.
        /// </summary>
        /// <remarks>
        /// If neither <see cref="Type"/> nor <see cref="TypeName"/> are specified,
        /// the type of the consumer is assumed.
        /// </remarks>
        public string? TypeName { get; }

        /// <summary>
        /// Gets the assembly name of the specified type.
        /// </summary>
        /// <remarks>
        /// <see cref="AssemblyName"/> is only valid when <see cref="TypeName"/> is specified.
        /// </remarks>
        public string? AssemblyName { get; }

        /// <summary>
        /// Gets or sets the condition in which the dependency is applicable, e.g. "DEBUG".
        /// </summary>
        public string? Condition { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMemberTypes { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies the types of members that are dynamically accessed.
    ///
    /// This enumeration has a <see cref="global::System.FlagsAttribute"/> attribute that allows a
    /// bitwise combination of its member values.
    /// </summary>
    [global::System.Flags]
    internal enum DynamicallyAccessedMemberTypes
    {
        /// <summary>
        /// Specifies no members.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies the default, parameterless public constructor.
        /// </summary>
        PublicParameterlessConstructor = 0x0001,

        /// <summary>
        /// Specifies all public constructors.
        /// </summary>
        PublicConstructors = 0x0002 | global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.PublicParameterlessConstructor,

        /// <summary>
        /// Specifies all non-public constructors.
        /// </summary>
        NonPublicConstructors = 0x0004,

        /// <summary>
        /// Specifies all public methods.
        /// </summary>
        PublicMethods = 0x0008,

        /// <summary>
        /// Specifies all non-public methods.
        /// </summary>
        NonPublicMethods = 0x0010,

        /// <summary>
        /// Specifies all public fields.
        /// </summary>
        PublicFields = 0x0020,

        /// <summary>
        /// Specifies all non-public fields.
        /// </summary>
        NonPublicFields = 0x0040,

        /// <summary>
        /// Specifies all public nested types.
        /// </summary>
        PublicNestedTypes = 0x0080,

        /// <summary>
        /// Specifies all non-public nested types.
        /// </summary>
        NonPublicNestedTypes = 0x0100,

        /// <summary>
        /// Specifies all public properties.
        /// </summary>
        PublicProperties = 0x0200,

        /// <summary>
        /// Specifies all non-public properties.
        /// </summary>
        NonPublicProperties = 0x0400,

        /// <summary>
        /// Specifies all public events.
        /// </summary>
        PublicEvents = 0x0800,

        /// <summary>
        /// Specifies all non-public events.
        /// </summary>
        NonPublicEvents = 0x1000,

        /// <summary>
        /// Specifies all interfaces implemented by the type.
        /// </summary>
        Interfaces = 0x2000,

        /// <summary>
        /// Specifies all members.
        /// </summary>
        All = ~global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes.None
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_DynamicallyAccessedMembersAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that certain members on a specified <see cref="global::System.Type"/> are accessed dynamically,
    /// for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which members are being accessed during the execution
    /// of a program.
    ///
    /// This attribute is valid on members whose type is <see cref="global::System.Type"/> or <see cref="string"/>.
    ///
    /// When this attribute is applied to a location of type <see cref="string"/>, the assumption is
    /// that the string represents a fully qualified type name.
    ///
    /// When this attribute is applied to a class, interface, or struct, the members specified
    /// can be accessed dynamically on <see cref="global::System.Type"/> instances returned from calling
    /// <see cref="object.GetType"/> on instances of that class, interface, or struct.
    ///
    /// If the attribute is applied to a method it's treated as a special case and it implies
    /// the attribute should be applied to the "this" parameter of the method. As such the attribute
    /// should only be used on instance methods of types assignable to System.Type (or string, but no methods
    /// will use it there).
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.ReturnValue |
        global::System.AttributeTargets.GenericParameter |
        global::System.AttributeTargets.Parameter |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Struct,
        Inherited = false)]
    internal sealed class DynamicallyAccessedMembersAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute"/> class
        /// with the specified member types.
        /// </summary>
        /// <param name="memberTypes">The types of members dynamically accessed.</param>
        public DynamicallyAccessedMembersAttribute(global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes)
        {
            MemberTypes = memberTypes;
        }

        /// <summary>
        /// Gets the <see cref="global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes"/> which specifies the type
        /// of members dynamically accessed.
        /// </summary>
        public global::System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MemberTypes { get; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MaybeNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that an output may be null even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal sealed class MaybeNullAttribute : Attribute
{ 
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MaybeNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that when a method returns <see cref="ReturnValue"/>, the parameter may be null even if the corresponding type disallows it.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class MaybeNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter may be null.
    /// </param>
    public MaybeNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
internal sealed class MemberNotNullAttribute : Attribute
{
    /// <summary>Initializes the attribute with a field or property member.</summary>
    /// <param name="member">
    /// The field or property member that is promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(string member) => Members = new[] { member };

    /// <summary>Initializes the attribute with the list of field and property members.</summary>
    /// <param name="members">
    /// The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(params string[] members) => Members = members;

    /// <summary>Gets field or property member names.</summary>
    public string[] Members { get; }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_MemberNotNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values when returning with the specified return value condition.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
internal sealed class MemberNotNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition and a field or property member.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    /// <param name="member">
    /// The field or property member that is promised to be not-null.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, string member)
    {
        ReturnValue = returnValue;
        Members = new[] { member };
    }

    /// <summary>Initializes the attribute with the specified return value condition and list of field and property members.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    /// <param name="members">
    /// The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
    {
        ReturnValue = returnValue;
        Members = members;
    }

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }

    /// <summary>Gets field or property member names.</summary>
    public string[] Members { get; }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that an output will not be null even if the corresponding type allows it. Specifies that an input argument was not null when the call returns.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal sealed class NotNullAttribute : Attribute
{
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullIfNotNullAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that the output will be non-null if the named parameter is non-null.</summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
internal sealed class NotNullIfNotNullAttribute : Attribute
{
    /// <summary>Initializes the attribute with the associated parameter name.</summary>
    /// <param name="parameterName">
    /// The associated parameter name.  The output will be non-null if the argument to the parameter specified is non-null.
    /// </param>
    public NotNullIfNotNullAttribute(string parameterName) => ParameterName = parameterName;

    /// <summary>Gets the associated parameter name.</summary>
    public string ParameterName { get; }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_NotNullWhenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>Specifies that when a method returns <see cref="ReturnValue"/>, the parameter will not be null even if the corresponding type allows it.</summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class NotNullWhenAttribute : Attribute
{
    /// <summary>Initializes the attribute with the specified return value condition.</summary>
    /// <param name="returnValue">
    /// The return value condition. If the method returns this value, the associated parameter will not be null.
    /// </param>
    public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

    /// <summary>Gets the return value condition.</summary>
    public bool ReturnValue { get; }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresAssemblyFilesAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified member requires assembly files to be on disk.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        Inherited = false, AllowMultiple = false)]
    internal sealed class RequiresAssemblyFilesAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.RequiresAssemblyFilesAttribute"/> class.
        /// </summary>
        public RequiresAssemblyFilesAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.RequiresAssemblyFilesAttribute"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the need for assembly files to be on disk.
        /// </param>
        public RequiresAssemblyFilesAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets an optional message that contains information about the need for
        /// assembly files to be on disk.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the member,
        /// why it requires assembly files to be on disk, and what options a consumer has
        /// to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresDynamicCodeAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified method requires the ability to generate new code at runtime,
    /// for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which methods are unsafe to call when compiling ahead of time.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Class,
        Inherited = false)]
    internal sealed class RequiresDynamicCodeAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.RequiresDynamicCodeAttribute"/> class
        /// with the specified message.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the usage of dynamic code.
        /// </param>
        public RequiresDynamicCodeAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a message that contains information about the usage of dynamic code.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the method,
        /// why it requires dynamic code, and what options a consumer has to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_RequiresUnreferencedCodeAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Indicates that the specified method requires dynamic access to code that is not referenced
    /// statically, for example through <see cref="global::System.Reflection"/>.
    /// </summary>
    /// <remarks>
    /// This allows tools to understand which methods are unsafe to call when removing unreferenced
    /// code from an application.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Class, Inherited = false)]
    internal sealed class RequiresUnreferencedCodeAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute"/> class
        /// with the specified message.
        /// </summary>
        /// <param name="message">
        /// A message that contains information about the usage of unreferenced code.
        /// </param>
        public RequiresUnreferencedCodeAttribute(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets a message that contains information about the usage of unreferenced code.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets an optional URL that contains more information about the method,
        /// why it requires unreferenced code, and what options a consumer has to deal with it.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_SetsRequiredMembersAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies that this constructor sets all required members for the current type, and callers
    /// do not need to set any required members themselves.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    internal sealed class SetsRequiredMembersAttribute : Attribute
    { }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_StringSyntaxAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>Specifies the syntax used in a string.</summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal sealed class StringSyntaxAttribute : Attribute
    {
        /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
        /// <param name="syntax">The syntax identifier.</param>
        public StringSyntaxAttribute(string syntax)
        {
            Syntax = syntax;
            Arguments = Array.Empty<object?>();
        }

        /// <summary>Initializes the <see cref="StringSyntaxAttribute"/> with the identifier of the syntax used.</summary>
        /// <param name="syntax">The syntax identifier.</param>
        /// <param name="arguments">Optional arguments associated with the specific syntax employed.</param>
        public StringSyntaxAttribute(string syntax, params object?[] arguments)
        {
            Syntax = syntax;
            Arguments = arguments;
        }

        /// <summary>Gets the identifier of the syntax used.</summary>
        public string Syntax { get; }

        /// <summary>Optional arguments associated with the specific syntax employed.</summary>
        public object?[] Arguments { get; }

        /// <summary>The syntax identifier for strings containing composite formats for string formatting.</summary>
        public const string CompositeFormat = nameof(CompositeFormat);

        /// <summary>The syntax identifier for strings containing date format specifiers.</summary>
        public const string DateOnlyFormat = nameof(DateOnlyFormat);

        /// <summary>The syntax identifier for strings containing date and time format specifiers.</summary>
        public const string DateTimeFormat = nameof(DateTimeFormat);

        /// <summary>The syntax identifier for strings containing <see cref="Enum"/> format specifiers.</summary>
        public const string EnumFormat = nameof(EnumFormat);

        /// <summary>The syntax identifier for strings containing <see cref="Guid"/> format specifiers.</summary>
        public const string GuidFormat = nameof(GuidFormat);

        /// <summary>The syntax identifier for strings containing JavaScript Object Notation (JSON).</summary>
        public const string Json = nameof(Json);

        /// <summary>The syntax identifier for strings containing numeric format specifiers.</summary>
        public const string NumericFormat = nameof(NumericFormat);

        /// <summary>The syntax identifier for strings containing regular expressions.</summary>
        public const string Regex = nameof(Regex);

        /// <summary>The syntax identifier for strings containing time format specifiers.</summary>
        public const string TimeOnlyFormat = nameof(TimeOnlyFormat);

        /// <summary>The syntax identifier for strings containing <see cref="TimeSpan"/> format specifiers.</summary>
        public const string TimeSpanFormat = nameof(TimeSpanFormat);

        /// <summary>The syntax identifier for strings containing URIs.</summary>
        public const string Uri = nameof(Uri);

        /// <summary>The syntax identifier for strings containing XML.</summary>
        public const string Xml = nameof(Xml);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_UnconditionalSuppressMessageAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Suppresses reporting of a specific rule violation, allowing multiple suppressions on a
    /// single code artifact.
    /// </summary>
    /// <remarks>
    /// <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/> is different than
    /// <see cref="global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute"/> in that it doesn't have a
    /// <see cref="global::System.Diagnostics.ConditionalAttribute"/>. So it is always preserved in the compiled assembly.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal sealed class UnconditionalSuppressMessageAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/>
        /// class, specifying the category of the tool and the identifier for an analysis rule.
        /// </summary>
        /// <param name="category">The category for the attribute.</param>
        /// <param name="checkId">The identifier of the analysis rule the attribute applies to.</param>
        public UnconditionalSuppressMessageAttribute(string category, string checkId)
        {
            Category = category;
            CheckId = checkId;
        }

        /// <summary>
        /// Gets the category identifying the classification of the attribute.
        /// </summary>
        /// <remarks>
        /// The <see cref="Category"/> property describes the tool or tool analysis category
        /// for which a message suppression attribute applies.
        /// </remarks>
        public string Category { get; }

        /// <summary>
        /// Gets the identifier of the analysis tool rule to be suppressed.
        /// </summary>
        /// <remarks>
        /// Concatenated together, the <see cref="Category"/> and <see cref="CheckId"/>
        /// properties form a unique check identifier.
        /// </remarks>
        public string CheckId { get; }

        /// <summary>
        /// Gets or sets the scope of the code that is relevant for the attribute.
        /// </summary>
        /// <remarks>
        /// The Scope property is an optional argument that specifies the metadata scope for which
        /// the attribute is relevant.
        /// </remarks>
        public string? Scope { get; set; }

        /// <summary>
        /// Gets or sets a fully qualified path that represents the target of the attribute.
        /// </summary>
        /// <remarks>
        /// The <see cref="Target"/> property is an optional argument identifying the analysis target
        /// of the attribute. An example value is "System.IO.Stream.ctor():System.Void".
        /// Because it is fully qualified, it can be long, particularly for targets such as parameters.
        /// The analysis tool user interface should be capable of automatically formatting the parameter.
        /// </remarks>
        public string? Target { get; set; }

        /// <summary>
        /// Gets or sets an optional argument expanding on exclusion criteria.
        /// </summary>
        /// <remarks>
        /// The <see cref="MessageId "/> property is an optional argument that specifies additional
        /// exclusion where the literal metadata target is not sufficiently precise. For example,
        /// the <see cref="global::System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute"/> cannot be applied within a method,
        /// and it may be desirable to suppress a violation against a statement in the method that will
        /// give a rule violation, but not against all statements in the method.
        /// </remarks>
        public string? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the justification for suppressing the code analysis message.
        /// </summary>
        public string? Justification { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_CodeAnalysis_UnscopedRefAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Used to indicate a byref escapes and is not scoped.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There are several cases where the C# compiler treats a <see langword="ref"/> as implicitly
    /// <see langword="scoped"/> - where the compiler does not allow the <see langword="ref"/> to escape the method.
    /// </para>
    /// <para>
    /// For example:
    /// <list type="number">
    ///     <item><see langword="this"/> for <see langword="struct"/> instance methods.</item>
    ///     <item><see langword="ref"/> parameters that refer to <see langword="ref"/> <see langword="struct"/> types.</item>
    ///     <item><see langword="out"/> parameters.</item>
    /// </list>
    /// </para>
    /// <para>
    /// This attribute is used in those instances where the <see langword="ref"/> should be allowed to escape.
    /// </para>
    /// <para>
    /// Applying this attribute, in any form, has impact on consumers of the applicable API. It is necessary for
    /// API authors to understand the lifetime implications of applying this attribute and how it may impact their users.
    /// </para>
    /// </remarks>
    [AttributeUsageAttribute(
        AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter,
        AllowMultiple = false,
        Inherited = false)]
    internal sealed class UnscopedRefAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnscopedRefAttribute"/> class.
        /// </summary>
        public UnscopedRefAttribute() { }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Diagnostics_StackTraceHiddenAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Diagnostics
{
    /// <summary>
    /// Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in StackTrace.ToString()
    /// and Exception.StackTrace
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Struct,
        Inherited = false)]
    internal sealed class StackTraceHiddenAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Diagnostics.StackTraceHiddenAttribute"/> class.
        /// </summary>
        public StackTraceHiddenAttribute() { }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_HashCode { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*

The xxHash32 implementation is based on the code published by Yann Collet:
https://raw.githubusercontent.com/Cyan4973/xxHash/5c174cfa4e45a42f94082dc0d4539b39696afea1/xxhash.c

  xxHash - Fast Hash algorithm
  Copyright (C) 2012-2016, Yann Collet
  
  BSD 2-Clause License (http://www.opensource.org/licenses/bsd-license.php)
  
  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions are
  met:
  
  * Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following disclaimer
  in the documentation and/or other materials provided with the
  distribution.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
  LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
  
  You can contact the author at :
  - xxHash homepage: http://www.xxhash.com
  - xxHash source repository : https://github.com/Cyan4973/xxHash

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace System
{
    // xxHash32 is used for the hash code.
    // https://github.com/Cyan4973/xxHash

    internal struct HashCode
    {
        private static readonly uint s_seed = GenerateGlobalSeed();

        private const uint Prime1 = 2654435761U;
        private const uint Prime2 = 2246822519U;
        private const uint Prime3 = 3266489917U;
        private const uint Prime4 = 668265263U;
        private const uint Prime5 = 374761393U;

        private uint _v1, _v2, _v3, _v4;
        private uint _queue1, _queue2, _queue3;
        private uint _length;

        private static uint GenerateGlobalSeed()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] tmp = new byte[sizeof(uint)];
                rng.GetBytes(tmp);
                return (uint)BitConverter.ToInt32(tmp, startIndex: 0);
            }
        }

        public static int Combine<T1>(T1 value1)
        {
            // Provide a way of diffusing bits from something with a limited
            // input hash space. For example, many enums only have a few
            // possible hashes, only using the bottom few bits of the code. Some
            // collections are built on the assumption that hashes are spread
            // over a larger space, so diffusing the bits may help the
            // collection work more efficiently.

            var hc1 = (uint)(value1?.GetHashCode() ?? 0);

            uint hash = MixEmptyState();
            hash += 4;

            hash = QueueRound(hash, hc1);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);

            uint hash = MixEmptyState();
            hash += 8;

            hash = QueueRound(hash, hc1);
            hash = QueueRound(hash, hc2);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);

            uint hash = MixEmptyState();
            hash += 12;

            hash = QueueRound(hash, hc1);
            hash = QueueRound(hash, hc2);
            hash = QueueRound(hash, hc3);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);
            var hc4 = (uint)(value4?.GetHashCode() ?? 0);

            Initialize(out uint v1, out uint v2, out uint v3, out uint v4);

            v1 = Round(v1, hc1);
            v2 = Round(v2, hc2);
            v3 = Round(v3, hc3);
            v4 = Round(v4, hc4);

            uint hash = MixState(v1, v2, v3, v4);
            hash += 16;

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);
            var hc4 = (uint)(value4?.GetHashCode() ?? 0);
            var hc5 = (uint)(value5?.GetHashCode() ?? 0);

            Initialize(out uint v1, out uint v2, out uint v3, out uint v4);

            v1 = Round(v1, hc1);
            v2 = Round(v2, hc2);
            v3 = Round(v3, hc3);
            v4 = Round(v4, hc4);

            uint hash = MixState(v1, v2, v3, v4);
            hash += 20;

            hash = QueueRound(hash, hc5);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);
            var hc4 = (uint)(value4?.GetHashCode() ?? 0);
            var hc5 = (uint)(value5?.GetHashCode() ?? 0);
            var hc6 = (uint)(value6?.GetHashCode() ?? 0);

            Initialize(out uint v1, out uint v2, out uint v3, out uint v4);

            v1 = Round(v1, hc1);
            v2 = Round(v2, hc2);
            v3 = Round(v3, hc3);
            v4 = Round(v4, hc4);

            uint hash = MixState(v1, v2, v3, v4);
            hash += 24;

            hash = QueueRound(hash, hc5);
            hash = QueueRound(hash, hc6);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);
            var hc4 = (uint)(value4?.GetHashCode() ?? 0);
            var hc5 = (uint)(value5?.GetHashCode() ?? 0);
            var hc6 = (uint)(value6?.GetHashCode() ?? 0);
            var hc7 = (uint)(value7?.GetHashCode() ?? 0);

            Initialize(out uint v1, out uint v2, out uint v3, out uint v4);

            v1 = Round(v1, hc1);
            v2 = Round(v2, hc2);
            v3 = Round(v3, hc3);
            v4 = Round(v4, hc4);

            uint hash = MixState(v1, v2, v3, v4);
            hash += 28;

            hash = QueueRound(hash, hc5);
            hash = QueueRound(hash, hc6);
            hash = QueueRound(hash, hc7);

            hash = MixFinal(hash);
            return (int)hash;
        }

        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
        {
            var hc1 = (uint)(value1?.GetHashCode() ?? 0);
            var hc2 = (uint)(value2?.GetHashCode() ?? 0);
            var hc3 = (uint)(value3?.GetHashCode() ?? 0);
            var hc4 = (uint)(value4?.GetHashCode() ?? 0);
            var hc5 = (uint)(value5?.GetHashCode() ?? 0);
            var hc6 = (uint)(value6?.GetHashCode() ?? 0);
            var hc7 = (uint)(value7?.GetHashCode() ?? 0);
            var hc8 = (uint)(value8?.GetHashCode() ?? 0);

            Initialize(out uint v1, out uint v2, out uint v3, out uint v4);

            v1 = Round(v1, hc1);
            v2 = Round(v2, hc2);
            v3 = Round(v3, hc3);
            v4 = Round(v4, hc4);

            v1 = Round(v1, hc5);
            v2 = Round(v2, hc6);
            v3 = Round(v3, hc7);
            v4 = Round(v4, hc8);

            uint hash = MixState(v1, v2, v3, v4);
            hash += 32;

            hash = MixFinal(hash);
            return (int)hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
        {
            v1 = s_seed + Prime1 + Prime2;
            v2 = s_seed + Prime2;
            v3 = s_seed;
            v4 = s_seed - Prime1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Round(uint hash, uint input)
        {
            return BitOperations.RotateLeft(hash + input * Prime2, 13) * Prime1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint QueueRound(uint hash, uint queuedValue)
        {
            return BitOperations.RotateLeft(hash + queuedValue * Prime3, 17) * Prime4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MixState(uint v1, uint v2, uint v3, uint v4)
        {
            return BitOperations.RotateLeft(v1, 1) + BitOperations.RotateLeft(v2, 7) + BitOperations.RotateLeft(v3, 12) + BitOperations.RotateLeft(v4, 18);
        }

        private static uint MixEmptyState()
        {
            return s_seed + Prime5;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint MixFinal(uint hash)
        {
            hash ^= hash >> 15;
            hash *= Prime2;
            hash ^= hash >> 13;
            hash *= Prime3;
            hash ^= hash >> 16;
            return hash;
        }

        public void Add<T>(T value)
        {
            Add(value?.GetHashCode() ?? 0);
        }

        public void Add<T>(T value, IEqualityComparer<T>? comparer)
        {
            Add(comparer != null ? comparer.GetHashCode(value) : (value?.GetHashCode() ?? 0));
        }

        private void Add(int value)
        {
            // The original xxHash works as follows:
            // 0. Initialize immediately. We can't do this in a struct (no
            //    default ctor).
            // 1. Accumulate blocks of length 16 (4 uints) into 4 accumulators.
            // 2. Accumulate remaining blocks of length 4 (1 uint) into the
            //    hash.
            // 3. Accumulate remaining blocks of length 1 into the hash.

            // There is no need for #3 as this type only accepts ints. _queue1,
            // _queue2 and _queue3 are basically a buffer so that when
            // ToHashCode is called we can execute #2 correctly.

            // We need to initialize the xxHash32 state (_v1 to _v4) lazily (see
            // #0) nd the last place that can be done if you look at the
            // original code is just before the first block of 16 bytes is mixed
            // in. The xxHash32 state is never used for streams containing fewer
            // than 16 bytes.

            // To see what's really going on here, have a look at the Combine
            // methods.

            var val = (uint)value;

            // Storing the value of _length locally shaves of quite a few bytes
            // in the resulting machine code.
            uint previousLength = _length++;
            uint position = previousLength % 4;

            // Switch can't be inlined.

            if (position == 0)
                _queue1 = val;
            else if (position == 1)
                _queue2 = val;
            else if (position == 2)
                _queue3 = val;
            else // position == 3
            {
                if (previousLength == 3)
                    Initialize(out _v1, out _v2, out _v3, out _v4);

                _v1 = Round(_v1, _queue1);
                _v2 = Round(_v2, _queue2);
                _v3 = Round(_v3, _queue3);
                _v4 = Round(_v4, val);
            }
        }

        public int ToHashCode()
        {
            // Storing the value of _length locally shaves of quite a few bytes
            // in the resulting machine code.
            uint length = _length;

            // position refers to the *next* queue position in this method, so
            // position == 1 means that _queue1 is populated; _queue2 would have
            // been populated on the next call to Add.
            uint position = length % 4;

            // If the length is less than 4, _v1 to _v4 don't contain anything
            // yet. xxHash32 treats this differently.

            uint hash = length < 4 ? MixEmptyState() : MixState(_v1, _v2, _v3, _v4);

            // _length is incremented once per Add(Int32) and is therefore 4
            // times too small (xxHash length is in bytes, not ints).

            hash += length * 4;

            // Mix what remains in the queue

            // Switch can't be inlined right now, so use as few branches as
            // possible by manually excluding impossible scenarios (position > 1
            // is always false if position is not > 0).
            if (position > 0)
            {
                hash = QueueRound(hash, _queue1);
                if (position > 1)
                {
                    hash = QueueRound(hash, _queue2);
                    if (position > 2)
                        hash = QueueRound(hash, _queue3);
                }
            }

            hash = MixFinal(hash);
            return (int)hash;
        }

#pragma warning disable 0809
        // Obsolete member 'memberA' overrides non-obsolete member 'memberB'. 
        // Disallowing GetHashCode and Equals is by design

        // * We decided to not override GetHashCode() to produce the hash code 
        //   as this would be weird, both naming-wise as well as from a
        //   behavioral standpoint (GetHashCode() should return the object's
        //   hash code, not the one being computed).

        // * Even though ToHashCode() can be called safely multiple times on
        //   this implementation, it is not part of the contract. If the
        //   implementation has to change in the future we don't want to worry
        //   about people who might have incorrectly used this type.

        [Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", error: true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.");

        [Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", error: true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes.");
#pragma warning restore 0809
    }
}

file static partial class LocalAppContextSwitches
{
    private static int s_useNonRandomizedHashSeed;
    public static bool UseNonRandomizedHashSeed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetCachedSwitchValue("Switch.System.Data.UseNonRandomizedHashSeed", ref s_useNonRandomizedHashSeed);
    }
}

// Helper method for local caching of compatibility quirks. Keep this lean and simple - this file is included into
// every framework assembly that implements any compatibility quirks.
file static partial class LocalAppContextSwitches
{
    // Returns value of given switch using provided cache.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool GetCachedSwitchValue(string switchName, ref int cachedSwitchValue)
    {
        // The cached switch value has 3 states: 0 - unknown, 1 - true, -1 - false
        if (cachedSwitchValue < 0) return false;
        if (cachedSwitchValue > 0) return true;

        return GetCachedSwitchValueInternal(switchName, ref cachedSwitchValue);
    }

    private static bool GetCachedSwitchValueInternal(string switchName, ref int cachedSwitchValue)
    {
        bool isSwitchEnabled;

        bool hasSwitch = AppContext.TryGetSwitch(switchName, out isSwitchEnabled);
        if (!hasSwitch)
        {
            isSwitchEnabled = GetSwitchDefaultValue(switchName);
        }

        AppContext.TryGetSwitch(@"TestSwitch.LocalAppContext.DisableCaching", out bool disableCaching);
        if (!disableCaching)
        {
            cachedSwitchValue = isSwitchEnabled ? 1 /*true*/ : -1 /*false*/;
        }

        return isSwitchEnabled;
    }

    // Provides default values for switches if they're not always false by default
    private static bool GetSwitchDefaultValue(string switchName)
    {
        if (switchName == "Switch.System.Runtime.Serialization.SerializationGuard")
        {
            return true;
        }

        return false;
    }
}

// NOTE: This class is a copy from src\Common\src\CoreLib\System\Numerics\BitOperations.cs only for HashCode purposes.
// Any changes to the BitOperations class should be done in there instead.
file static class BitOperations
{
    /// <summary>
    /// Rotates the specified value left by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint RotateLeft(uint value, int offset)
        => (value << offset) | (value >> (32 - offset));

    /// <summary>
    /// Rotates the specified value left by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong RotateLeft(ulong value, int offset)
        => (value << offset) | (value >> (64 - offset));
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Index { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System
{
    /// <summary>Represent a type can be used to index a collection either from the start or the end.</summary>
    /// <remarks>
    /// Index is used by the C# compiler to support the new index syntax
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 } ;
    /// int lastElement = someArray[^1]; // lastElement = 5
    /// </code>
    /// </remarks>
    internal readonly struct Index : global::System.IEquatable<global::System.Index>
    {
        private readonly int _value;

        /// <summary>Construct an Index using a value and indicating if the index is from the start or from the end.</summary>
        /// <param name="value">The index value. it has to be zero or positive number.</param>
        /// <param name="fromEnd">Indicating if the index is from the start or from the end.</param>
        /// <remarks>
        /// If the Index constructed from the end, index value 1 means pointing at the last element and index value 0 means pointing at beyond last element.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            if (fromEnd)
                _value = ~value;
            else
                _value = value;
        }

        // The following private constructors mainly created for perf reason to avoid the checks
        private Index(int value)
        {
            _value = value;
        }

        /// <summary>Create an Index pointing at first element.</summary>
        public static global::System.Index Start => new global::System.Index(0);

        /// <summary>Create an Index pointing at beyond last element.</summary>
        public static global::System.Index End => new global::System.Index(~0);

        /// <summary>Create an Index from the start at the position indicated by the value.</summary>
        /// <param name="value">The index value from the start.</param>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static global::System.Index FromStart(int value)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            return new global::System.Index(value);
        }

        /// <summary>Create an Index from the end at the position indicated by the value.</summary>
        /// <param name="value">The index value from the end.</param>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static global::System.Index FromEnd(int value)
        {
            if (value < 0)
            {
                global::System.Index.ThrowHelper.ThrowValueArgumentOutOfRange_NeedNonNegNumException();
            }

            return new global::System.Index(~value);
        }

        /// <summary>Returns the index value.</summary>
        public int Value
        {
            get
            {
                if (_value < 0)
                    return ~_value;
                else
                    return _value;
            }
        }

        /// <summary>Indicates whether the index is from the start or the end.</summary>
        public bool IsFromEnd => _value < 0;

        /// <summary>Calculate the offset from the start using the giving collection length.</summary>
        /// <param name="length">The length of the collection that the Index will be used with. length has to be a positive value</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter and the returned offset value against negative values.
        /// we don't validate either the returned offset is greater than the input length.
        /// It is expected Index will be used with collections which always have non negative length/count. If the returned offset is negative and
        /// then used to index a collection will get out of range exception which will be same affect as the validation.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            int offset = _value;
            if (IsFromEnd)
            {
                // offset = length - (~value)
                // offset = length + (~(~value) + 1)
                // offset = length + value + 1

                offset += length + 1;
            }
            return offset;
        }

        /// <summary>Indicates whether the current Index object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
        public override bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? value) => value is global::System.Index && _value == ((global::System.Index)value)._value;

        /// <summary>Indicates whether the current Index object is equal to another Index object.</summary>
        /// <param name="other">An object to compare with this object</param>
        public bool Equals(global::System.Index other) => _value == other._value;

        /// <summary>Returns the hash code for this instance.</summary>
        public override int GetHashCode() => _value;

        /// <summary>Converts integer number to an Index.</summary>
        public static implicit operator global::System.Index(int value) => FromStart(value);

        /// <summary>Converts the value of the current Index object to its equivalent string representation.</summary>
        public override string ToString()
        {
            if (IsFromEnd)
                return ToStringFromEnd();

            return ((uint)Value).ToString();
        }

        private string ToStringFromEnd()
        {
            return '^' + Value.ToString();
        }

        private static class ThrowHelper
        {
            [global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
            public static void ThrowValueArgumentOutOfRange_NeedNonNegNumException()
            {
                throw new global::System.ArgumentOutOfRangeException("value", "Non-negative number required.");
            }
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Range { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System
{
    /// <summary>Represent a range has start and end indexes.</summary>
    /// <remarks>
    /// Range is used by the C# compiler to support the range syntax.
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
    /// int[] subArray1 = someArray[0..2]; // { 1, 2 }
    /// int[] subArray2 = someArray[1..^0]; // { 2, 3, 4, 5 }
    /// </code>
    /// </remarks>
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal readonly struct Range : global::System.IEquatable<global::System.Range>
    {
        /// <summary>Represent the inclusive start index of the Range.</summary>
        public global::System.Index Start { get; }

        /// <summary>Represent the exclusive end index of the Range.</summary>
        public global::System.Index End { get; }

        /// <summary>Construct a Range object using the start and end indexes.</summary>
        /// <param name="start">Represent the inclusive start index of the range.</param>
        /// <param name="end">Represent the exclusive end index of the range.</param>
        public Range(global::System.Index start, global::System.Index end)
        {
            Start = start;
            End = end;
        }

        /// <summary>Indicates whether the current Range object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object</param>
        public override bool Equals([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? value) =>
            value is global::System.Range r &&
            r.Start.Equals(Start) &&
            r.End.Equals(End);

        /// <summary>Indicates whether the current Range object is equal to another Range object.</summary>
        /// <param name="other">An object to compare with this object</param>
        public bool Equals(global::System.Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        /// <summary>Returns the hash code for this instance.</summary>
        public override int GetHashCode()
        {
            return global::System.Range.HashHelpers.Combine(Start.GetHashCode(), End.GetHashCode());
        }

        /// <summary>Converts the value of the current Range object to its equivalent string representation.</summary>
        public override string ToString()
        {
            return Start.ToString() + ".." + End.ToString();
        }

        /// <summary>Create a Range object starting from start index to the end of the collection.</summary>
        public static global::System.Range StartAt(global::System.Index start) => new global::System.Range(start, global::System.Index.End);

        /// <summary>Create a Range object starting from first element in the collection to the end Index.</summary>
        public static global::System.Range EndAt(global::System.Index end) => new global::System.Range(global::System.Index.Start, end);

        /// <summary>Create a Range object starting from first element to the end.</summary>
        public static global::System.Range All => new global::System.Range(global::System.Index.Start, global::System.Index.End);

        /// <summary>Calculate the start offset and length of range object using a collection length.</summary>
        /// <param name="length">The length of the collection that the range will be used with. length has to be a positive value.</param>
        /// <remarks>
        /// For performance reason, we don't validate the input length parameter against negative values.
        /// It is expected Range will be used with collections which always have non negative length/count.
        /// We validate the range is inside the length scope though.
        /// </remarks>
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start;
            global::System.Index startIndex = Start;
            if (startIndex.IsFromEnd)
                start = length - startIndex.Value;
            else
                start = startIndex.Value;

            int end;
            global::System.Index endIndex = End;
            if (endIndex.IsFromEnd)
                end = length - endIndex.Value;
            else
                end = endIndex.Value;

            if ((uint)end > (uint)length || (uint)start > (uint)end)
            {
                global::System.Range.ThrowHelper.ThrowArgumentOutOfRangeException();
            }

            return (start, end - start);
        }

        private static class HashHelpers
        {
            public static int Combine(int h1, int h2)
            {
                uint rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
                return ((int)rol5 + h1) ^ h2;
            }
        }

        private static class ThrowHelper
        {
            [global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
            public static void ThrowArgumentOutOfRangeException()
            {
                throw new global::System.ArgumentOutOfRangeException("length");
            }
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_AsyncMethodBuilderAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates the type of the async method builder that should be used by a language compiler to
    /// build the attributed async method or to build the attributed type when used as the return type
    /// of an async method.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Delegate |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Method,
        Inherited = false, AllowMultiple = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class AsyncMethodBuilderAttribute : global::System.Attribute
    {
        /// <summary>Initializes the <see cref="global::System.Runtime.CompilerServices.AsyncMethodBuilderAttribute"/>.</summary>
        /// <param name="builderType">The <see cref="global::System.Type"/> of the associated builder.</param>
        public AsyncMethodBuilderAttribute(global::System.Type builderType) => BuilderType = builderType;

        /// <summary>Gets the <see cref="global::System.Type"/> of the associated builder.</summary>
        public global::System.Type BuilderType { get; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_CallerArgumentExpressionAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// An attribute that allows parameters to receive the expression of other parameters.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/> class.
        /// </summary>
        /// <param name="parameterName">The condition parameter value.</param>
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Gets the parameter name the expression is retrieved from.
        /// </summary>
        public string ParameterName { get; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_CompilerFeatureRequiredAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates that compiler support for a particular feature is required for the location where this attribute is applied.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    internal sealed class CompilerFeatureRequiredAttribute : global::System.Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="global::System.Runtime.CompilerServices.CompilerFeatureRequiredAttribute"/> type.
        /// </summary>
        /// <param name="featureName">The name of the feature to indicate.</param>
        public CompilerFeatureRequiredAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        /// <summary>
        /// The name of the compiler feature.
        /// </summary>
        public string FeatureName { get; }

        /// <summary>
        /// If true, the compiler can choose to allow access to the location where this attribute is applied if it does not understand <see cref="FeatureName"/>.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// The <see cref="FeatureName"/> used for the ref structs C# feature.
        /// </summary>
        public const string RefStructs = nameof(RefStructs);

        /// <summary>
        /// The <see cref="FeatureName"/> used for the required members C# feature.
        /// </summary>
        public const string RequiredMembers = nameof(RequiredMembers);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_DisableRuntimeMarshallingAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Disables the built-in runtime managed/unmanaged marshalling subsystem for
    /// P/Invokes, Delegate types, and unmanaged function pointer invocations.
    /// </summary>
    /// <remarks>
    /// The built-in marshalling subsystem has some behaviors that cannot be changed due to
    /// backward-compatibility requirements. This attribute allows disabling the built-in
    /// subsystem and instead uses the following rules for P/Invokes, Delegates,
    /// and unmanaged function pointer invocations:
    ///
    /// - All value types that do not contain reference type fields recursively (<c>unmanaged</c> in C#) are blittable
    /// - Value types that recursively have any fields that have <c>[StructLayout(LayoutKind.Auto)]</c> are disallowed from interop.
    /// - All reference types are disallowed from usage in interop scenarios.
    /// - SetLastError support in P/Invokes is disabled.
    /// - varargs support is disabled.
    /// - LCIDConversionAttribute support is disabled.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    internal sealed class DisableRuntimeMarshallingAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerArgumentAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates which arguments to a method involving an interpolated string handler should be passed to that handler.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class InterpolatedStringHandlerArgumentAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute"/> class.
        /// </summary>
        /// <param name="argument">The name of the argument that should be passed to the handler.</param>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public InterpolatedStringHandlerArgumentAttribute(string argument)
        {
            Arguments = new string[] { argument };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.CompilerServices.InterpolatedStringHandlerArgumentAttribute"/> class.
        /// </summary>
        /// <param name="arguments">The names of the arguments that should be passed to the handler.</param>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public InterpolatedStringHandlerArgumentAttribute(params string[] arguments)
        {
            Arguments = arguments;
        }

        /// <summary>
        /// Gets the names of the arguments that should be passed to the handler.
        /// </summary>
        /// <remarks><see langword="null"/> may be used as the name of the receiver in an instance method.</remarks>
        public string[] Arguments { get; }
    }
}

"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_InterpolatedStringHandlerAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates the attributed type is to be used as an interpolated string handler.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct,
        AllowMultiple = false, Inherited = false)]
    internal sealed class InterpolatedStringHandlerAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_IsExternalInit { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// This class should not be used by developers in source code.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_ModuleInitializerAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that a method should be called
    /// in its containing module's initializer.
    /// </summary>
    /// <remarks>
    /// When one or more valid methods
    /// with this attribute are found in a compilation, the compiler will
    /// emit a module initializer which calls each of the attributed methods.
    ///
    /// Certain requirements are imposed on any method targeted with this attribute:
    /// - The method must be `static`.
    /// - The method must be an ordinary member method, as opposed to a property accessor, constructor, local function, etc.
    /// - The method must be parameterless.
    /// - The method must return `void`.
    /// - The method must not be generic or be contained in a generic type.
    /// - The method's effective accessibility must be `internal` or `public`.
    ///
    /// The specification for module initializers in the .NET runtime can be found here:
    /// https://github.com/dotnet/runtime/blob/main/docs/design/specs/Ecma-335-Augments.md#module-initializer
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : global::System.Attribute
    {
        public ModuleInitializerAttribute()
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_RequiredMemberAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Specifies that a type has required members or that a member is required.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class RequiredMemberAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_SkipLocalsInitAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Used to indicate to the compiler that the <c>.locals init</c> flag should not be set in method headers.
    /// </summary>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Event,
        Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class SkipLocalsInitAttribute : global::System.Attribute
    {
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_CompilerServices_TupleElementNamesAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Indicates that the use of <see cref="ValueTuple"/> on a member is meant to be treated as a tuple with element names.
    /// </summary>
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Event)]
    internal sealed class TupleElementNamesAttribute : Attribute
    {
        private readonly string?[] _transformNames;

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="TupleElementNamesAttribute"/> class.
        /// </summary>
        /// <param name="transformNames">
        /// Specifies, in a pre-order depth-first traversal of a type's
        /// construction, which <see cref="ValueType"/> occurrences are
        /// meant to carry element names.
        /// </param>
        /// <remarks>
        /// This constructor is meant to be used on types that contain an
        /// instantiation of <see cref="ValueType"/> that contains
        /// element names.  For instance, if <c>C</c> is a generic type with
        /// two type parameters, then a use of the constructed type <c>C{<see
        /// cref="ValueTuple{T1, T2}"/>, <see
        /// cref="ValueTuple{T1, T2, T3}"/></c> might be intended to
        /// treat the first type argument as a tuple with element names and the
        /// second as a tuple without element names. In which case, the
        /// appropriate attribute specification should use a
        /// <c>transformNames</c> value of <c>{ "name1", "name2", null, null,
        /// null }</c>.
        /// </remarks>
        public TupleElementNamesAttribute(string?[] transformNames)
        {
            if (transformNames == null)
                throw new ArgumentNullException(nameof(transformNames));

            _transformNames = transformNames;
        }

        /// <summary>
        /// Specifies, in a pre-order depth-first traversal of a type's
        /// construction, which <see cref="ValueTuple"/> elements are
        /// meant to carry element names.
        /// </summary>
        public IList<string?> TransformNames => _transformNames;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_InteropServices_SuppressGCTransitionAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices
{
    /// <summary>
    /// An attribute used to indicate a GC transition should be skipped when making an unmanaged function call.
    /// </summary>
    /// <example>
    /// Example of a valid use case. The Win32 `GetTickCount()` function is a small performance related function
    /// that reads some global memory and returns the value. In this case, the GC transition overhead is significantly
    /// more than the memory read.
    /// <code>
    /// using System;
    /// using System.Runtime.InteropServices;
    /// class Program
    /// {
    ///     [DllImport("Kernel32")]
    ///     [SuppressGCTransition]
    ///     static extern int GetTickCount();
    ///     static void Main()
    ///     {
    ///         Console.WriteLine($"{GetTickCount()}");
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// This attribute is ignored if applied to a method without the <see cref="global::System.Runtime.InteropServices.DllImportAttribute"/>.
    ///
    /// Forgoing this transition can yield benefits when the cost of the transition is more than the execution time
    /// of the unmanaged function. However, avoiding this transition removes some of the guarantees the runtime
    /// provides through a normal P/Invoke. When exiting the managed runtime to enter an unmanaged function the
    /// GC must transition from Cooperative mode into Preemptive mode. Full details on these modes can be found at
    /// https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/clr-code-guide.md#2.1.8.
    /// Suppressing the GC transition is an advanced scenario and should not be done without fully understanding
    /// potential consequences.
    ///
    /// One of these consequences is an impact to Mixed-mode debugging (https://docs.microsoft.com/visualstudio/debugger/how-to-debug-in-mixed-mode).
    /// During Mixed-mode debugging, it is not possible to step into or set breakpoints in a P/Invoke that
    /// has been marked with this attribute. A workaround is to switch to native debugging and set a breakpoint in the native function.
    /// In general, usage of this attribute is not recommended if debugging the P/Invoke is important, for example
    /// stepping through the native code or diagnosing an exception thrown from the native code.
    ///
    /// The runtime may load the native library for method marked with this attribute in advance before the method is called for the first time.
    /// Usage of this attribute is not recommended for platform neutral libraries with conditional platform specific code.
    ///
    /// The P/Invoke method that this attribute is applied to must have all of the following properties:
    ///   * Native function always executes for a trivial amount of time (less than 1 microsecond).
    ///   * Native function does not perform a blocking syscall (e.g. any type of I/O).
    ///   * Native function does not call back into the runtime (e.g. Reverse P/Invoke).
    ///   * Native function does not throw exceptions.
    ///   * Native function does not manipulate locks or other concurrency primitives.
    ///
    /// Consequences of invalid uses of this attribute:
    ///   * GC starvation.
    ///   * Immediate runtime termination.
    ///   * Data corruption.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class SuppressGCTransitionAttribute : global::System.Attribute
    {
        public SuppressGCTransitionAttribute()
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_InteropServices_UnmanagedCallersOnlyAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices
{
    /// <summary>
    /// Any method marked with <see cref="global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute" /> can be directly called from
    /// native code. The function token can be loaded to a local variable using the <see href="https://docs.microsoft.com/dotnet/csharp/language-reference/operators/pointer-related-operators#address-of-operator-">address-of</see> operator
    /// in C# and passed as a callback to a native method.
    /// </summary>
    /// <remarks>
    /// Methods marked with this attribute have the following restrictions:
    ///   * Method must be marked "static".
    ///   * Must not be called from managed code.
    ///   * Must only have <see href="https://docs.microsoft.com/dotnet/framework/interop/blittable-and-non-blittable-types">blittable</see> arguments.
    /// </remarks>
    [global::System.AttributeUsage(global::System.AttributeTargets.Method, Inherited = false)]
    internal sealed class UnmanagedCallersOnlyAttribute : global::System.Attribute
    {
        public UnmanagedCallersOnlyAttribute()
        {
        }

        /// <summary>
        /// Optional. If omitted, the runtime will use the default platform calling convention.
        /// </summary>
        /// <remarks>
        /// Supplied types must be from the official "System.Runtime.CompilerServices" namespace and
        /// be of the form "CallConvXXX".
        /// </remarks>
        public global::System.Type[]? CallConvs;

        /// <summary>
        /// Optional. If omitted, no named export is emitted during compilation.
        /// </summary>
        public string? EntryPoint;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_ObsoletedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Marks APIs that were obsoleted in a given operating system version.
    /// </summary>
    /// <remarks>
    /// Primarily used by OS bindings to indicate APIs that should not be used anymore.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Struct,
        AllowMultiple = true, Inherited = false)]
    internal sealed class ObsoletedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public ObsoletedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }

        public ObsoletedOSPlatformAttribute(string platformName, string? message)
            //: base(platformName)
        {
            Message = message;
        }

        public string? Message { get; }

        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_RequiresPreviewFeaturesAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Delegate |
        global::System.AttributeTargets.Struct |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Field |
        AttributeTargets.Event, Inherited = false)]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal sealed class RequiresPreviewFeaturesAttribute : global::System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.Versioning.RequiresPreviewFeaturesAttribute"/> class.
        /// </summary>
        public RequiresPreviewFeaturesAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="global::System.Runtime.Versioning.RequiresPreviewFeaturesAttribute"/> class with the specified message.
        /// </summary>
        /// <param name="message">An optional message associated with this attribute instance.</param>
        public RequiresPreviewFeaturesAttribute(string? message)
        {
            Message = message;
        }

        /// <summary>
        /// Returns the optional message associated with this attribute instance.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Returns the optional URL associated with this attribute instance.
        /// </summary>
        public string? Url { get; set; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_SupportedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Records the operating system (and minimum version) that supports an API. Multiple attributes can be
    /// applied to indicate support on multiple operating systems.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.SupportedOSPlatformAttribute " />
    /// or use guards to prevent calls to APIs on unsupported operating systems.
    ///
    /// A given platform should only be specified once.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Struct,
        AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public SupportedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_SupportedOSPlatformGuardAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Annotates a custom guard field, property or method with a supported platform name and optional version.
    /// Multiple attributes can be applied to indicate guard for multiple supported platforms.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.SupportedOSPlatformGuardAttribute " /> to a field, property or method
    /// and use that field, property or method in a conditional or assert statements in order to safely call platform specific APIs.
    ///
    /// The type of the field or property should be boolean, the method return type should be boolean in order to be used as platform guard.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformGuardAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public SupportedOSPlatformGuardAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_TargetPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Records the platform that the project targeted.
    /// </summary>
    [global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    internal sealed class TargetPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public TargetPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_UnsupportedOSPlatformAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Marks APIs that were removed in a given operating system version.
    /// </summary>
    /// <remarks>
    /// Primarily used by OS bindings to indicate APIs that are only available in
    /// earlier versions.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Assembly |
        global::System.AttributeTargets.Class |
        global::System.AttributeTargets.Constructor |
        global::System.AttributeTargets.Enum |
        global::System.AttributeTargets.Event |
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Interface |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Module |
        global::System.AttributeTargets.Property |
        global::System.AttributeTargets.Struct,
        AllowMultiple = true, Inherited = false)]
    internal sealed class UnsupportedOSPlatformAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public UnsupportedOSPlatformAttribute(string platformName)
            //: base(platformName)
        {
        }
        public UnsupportedOSPlatformAttribute(string platformName, string? message)
            //: base(platformName)
        {
            Message = message;
        }

        public string? Message { get; }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_Runtime_Versioning_UnsupportedOSPlatformGuardAttribute { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.Versioning
{
    /// <summary>
    /// Annotates the custom guard field, property or method with an unsupported platform name and optional version.
    /// Multiple attributes can be applied to indicate guard for multiple unsupported platforms.
    /// </summary>
    /// <remarks>
    /// Callers can apply a <see cref="global::System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute " /> to a field, property or method
    /// and use that  field, property or method in a conditional or assert statements as a guard to safely call APIs unsupported on those platforms.
    ///
    /// The type of the field or property should be boolean, the method return type should be boolean in order to be used as platform guard.
    /// </remarks>
    [global::System.AttributeUsage(
        global::System.AttributeTargets.Field |
        global::System.AttributeTargets.Method |
        global::System.AttributeTargets.Property,
        AllowMultiple = true, Inherited = false)]
    internal sealed class UnsupportedOSPlatformGuardAttribute : Attribute // global::System.Runtime.Versioning.OSPlatformAttribute
    {
        public UnsupportedOSPlatformGuardAttribute(string platformName)
            //: base(platformName)
        {
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;

namespace System
{
    internal struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, ITupleInternal
    {
        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="ValueTuple"/>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple;
        }

        /// <summary>Returns a value indicating whether this instance is equal to a specified value.</summary>
        /// <param name="other">An instance to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
        public bool Equals(ValueTuple other)
        {
            return true;
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            return other is ValueTuple;
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return 0;
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple other)
        {
            return 0;
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return 0;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return 0;
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>()</c>.
        /// </remarks>
        public override string ToString()
        {
            return "()";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return 0;
        }

        string ITupleInternal.ToStringEnd()
        {
            return ")";
        }

        int ITupleInternal.Size => 0;

        /// <summary>Creates a new struct 0-tuple.</summary>
        /// <returns>A 0-tuple.</returns>
        public static ValueTuple Create() =>
            new ValueTuple();

        /// <summary>Creates a new struct 1-tuple, or singleton.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <returns>A 1-tuple (singleton) whose value is (item1).</returns>
        public static ValueTuple<T1> Create<T1>(T1 item1) =>
            new ValueTuple<T1>(item1);

        /// <summary>Creates a new struct 2-tuple, or pair.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <returns>A 2-tuple (pair) whose value is (item1, item2).</returns>
        public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) =>
            new ValueTuple<T1, T2>(item1, item2);

        /// <summary>Creates a new struct 3-tuple, or triple.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <param name="item3">The value of the third component of the tuple.</param>
        /// <returns>A 3-tuple (triple) whose value is (item1, item2, item3).</returns>
        public static ValueTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) =>
            new ValueTuple<T1, T2, T3>(item1, item2, item3);

        /// <summary>Creates a new struct 4-tuple, or quadruple.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
        /// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <param name="item3">The value of the third component of the tuple.</param>
        /// <param name="item4">The value of the fourth component of the tuple.</param>
        /// <returns>A 4-tuple (quadruple) whose value is (item1, item2, item3, item4).</returns>
        public static ValueTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) =>
            new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);

        /// <summary>Creates a new struct 5-tuple, or quintuple.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
        /// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
        /// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <param name="item3">The value of the third component of the tuple.</param>
        /// <param name="item4">The value of the fourth component of the tuple.</param>
        /// <param name="item5">The value of the fifth component of the tuple.</param>
        /// <returns>A 5-tuple (quintuple) whose value is (item1, item2, item3, item4, item5).</returns>
        public static ValueTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) =>
            new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);

        /// <summary>Creates a new struct 6-tuple, or sextuple.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
        /// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
        /// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
        /// <typeparam name="T6">The type of the sixth component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <param name="item3">The value of the third component of the tuple.</param>
        /// <param name="item4">The value of the fourth component of the tuple.</param>
        /// <param name="item5">The value of the fifth component of the tuple.</param>
        /// <param name="item6">The value of the sixth component of the tuple.</param>
        /// <returns>A 6-tuple (sextuple) whose value is (item1, item2, item3, item4, item5, item6).</returns>
        public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) =>
            new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);

        /// <summary>Creates a new struct 7-tuple, or septuple.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
        /// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
        /// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
        /// <typeparam name="T6">The type of the sixth component of the tuple.</typeparam>
        /// <typeparam name="T7">The type of the seventh component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <param name="item3">The value of the third component of the tuple.</param>
        /// <param name="item4">The value of the fourth component of the tuple.</param>
        /// <param name="item5">The value of the fifth component of the tuple.</param>
        /// <param name="item6">The value of the sixth component of the tuple.</param>
        /// <param name="item7">The value of the seventh component of the tuple.</param>
        /// <returns>A 7-tuple (septuple) whose value is (item1, item2, item3, item4, item5, item6, item7).</returns>
        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) =>
            new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_1 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;

namespace System
{
    internal struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1}"/> instance's first component.
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        public ValueTuple(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1> && Equals((ValueTuple<T1>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its field
        /// is equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1>)) return false;

            var objTuple = (ValueTuple<T1>)other;

            return comparer.Equals(Item1, objTuple.Item1);
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1>)other;

            return Comparer<T1>.Default.Compare(Item1, objTuple.Item1);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1> other)
        {
            return Comparer<T1>.Default.Compare(Item1, other.Item1);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1>)other;

            return comparer.Compare(Item1, objTuple.Item1);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return s_t1Comparer.GetHashCode(Item1);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1);
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1)</c>,
        /// where <c>Item1</c> represents the value of <see cref="Item1"/>. If the field is <see langword="null"/>,
        /// it is represented as <see cref="string.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return comparer.GetHashCode(Item1);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ")";
        }

        int ITupleInternal.Size => 1;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_2 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 2-tuple, or pair, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2>
        : IEquatable<ValueTuple<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2}"/> instance's first component.
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2}"/> instance's second component.
        /// </summary>
        public T2 Item2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public ValueTuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        ///
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2> && Equals((ValueTuple<T1, T2>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2}"/> instance is equal to a specified <see cref="ValueTuple{T1, T2}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2}"/> instance is equal to a specified object based on a specified comparison method.
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        ///
        /// <remarks>
        /// This member is an explicit interface member implementation. It can be used only when the
        ///  <see cref="ValueTuple{T1, T2}"/> instance is cast to an <see cref="IStructuralEquatable"/> interface.
        ///
        /// The <see cref="IEqualityComparer.Equals"/> implementation is called only if <c>other</c> is not <see langword="null"/>,
        ///  and if it can be successfully cast (in C#) or converted (in Visual Basic) to a <see cref="ValueTuple{T1, T2}"/>
        ///  whose components are of the same types as those of the current instance. The IStructuralEquatable.Equals(Object, IEqualityComparer) method
        ///  first passes the <see cref="Item1"/> values of the <see cref="ValueTuple{T1, T2}"/> objects to be compared to the
        ///  <see cref="IEqualityComparer.Equals"/> implementation. If this method call returns <see langword="true"/>, the method is
        ///  called again and passed the <see cref="Item2"/> values of the two <see cref="ValueTuple{T1, T2}"/> instances.
        /// </remarks>
        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2>)) return false;

            var objTuple = (ValueTuple<T1, T2>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2);
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            return Comparer<T2>.Default.Compare(Item2, other.Item2);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            return comparer.Compare(Item2, objTuple.Item2);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!), comparer.GetHashCode(Item2!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2)</c>,
        /// where <c>Item1</c> and <c>Item2</c> represent the values of the <see cref="Item1"/>
        /// and <see cref="Item2"/> fields. If either field value is <see langword="null"/>,
        /// it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ")";
        }

        int ITupleInternal.Size => 2;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_3 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 3-tuple, or triple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3>
        : IEquatable<ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3}"/> instance's third component.
        /// </summary>
        public T3 Item3;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2, T3> && Equals((ValueTuple<T1, T2, T3>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3);
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            return Comparer<T3>.Default.Compare(Item3, other.Item3);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            return comparer.Compare(Item3, objTuple.Item3);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!), comparer.GetHashCode(Item2!), comparer.GetHashCode(Item3!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ")";
        }

        int ITupleInternal.Size => 3;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_4 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 4-tuple, or quadruple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4>
        : IEquatable<ValueTuple<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4> && Equals((ValueTuple<T1, T2, T3, T4>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4);
        }

        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            return Comparer<T4>.Default.Compare(Item4, other.Item4);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            return comparer.Compare(Item4, objTuple.Item4);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3, Item4);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                                    comparer.GetHashCode(Item2!),
                                    comparer.GetHashCode(Item3!),
                                    comparer.GetHashCode(Item4!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }
        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ")";
        }

        int ITupleInternal.Size => 4;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_5 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 5-tuple, or quintuple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4, T5>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;
        private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5> && Equals((ValueTuple<T1, T2, T3, T4, T5>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4, T5> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4)
                && s_t5Comparer.Equals(Item5, other.Item5);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4)
                && comparer.Equals(Item5, objTuple.Item5);
        }

        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4, T5>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            return Comparer<T5>.Default.Compare(Item5, other.Item5);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            c = comparer.Compare(Item4, objTuple.Item4);
            if (c != 0) return c;

            return comparer.Compare(Item5, objTuple.Item5);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                                    comparer.GetHashCode(Item2!),
                                    comparer.GetHashCode(Item3!),
                                    comparer.GetHashCode(Item4!),
                                    comparer.GetHashCode(Item5!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4, T5}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4, Item5)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ")";
        }

        int ITupleInternal.Size => 5;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_6 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{

    /// <summary>
    /// Represents a 6-tuple, or sixtuple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
    /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;
        private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;
        private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6> && Equals((ValueTuple<T1, T2, T3, T4, T5, T6>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4)
                && s_t5Comparer.Equals(Item5, other.Item5)
                && s_t6Comparer.Equals(Item6, other.Item6);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4)
                && comparer.Equals(Item5, objTuple.Item5)
                && comparer.Equals(Item6, objTuple.Item6);
        }

        int IComparable.CompareTo(object? other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            return Comparer<T6>.Default.Compare(Item6, other.Item6);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            c = comparer.Compare(Item4, objTuple.Item4);
            if (c != 0) return c;

            c = comparer.Compare(Item5, objTuple.Item5);
            if (c != 0) return c;

            return comparer.Compare(Item6, objTuple.Item6);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                                    comparer.GetHashCode(Item2!),
                                    comparer.GetHashCode(Item3!),
                                    comparer.GetHashCode(Item4!),
                                    comparer.GetHashCode(Item5!),
                                    comparer.GetHashCode(Item6!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4, Item5, Item6)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ")";
        }

        int ITupleInternal.Size => 6;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_7 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Represents a 7-tuple, or sentuple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
    /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
    /// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6, T7>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, ITupleInternal
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;
        private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;
        private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;
        private static readonly EqualityComparer<T7> s_t7Comparer = EqualityComparer<T7>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance's seventh component.
        /// </summary>
        public T7 Item7;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        /// <param name="item7">The value of the tuple's seventh component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7> && Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4)
                && s_t5Comparer.Equals(Item5, other.Item5)
                && s_t6Comparer.Equals(Item6, other.Item6)
                && s_t7Comparer.Equals(Item7, other.Item7);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4)
                && comparer.Equals(Item5, objTuple.Item5)
                && comparer.Equals(Item6, objTuple.Item6)
                && comparer.Equals(Item7, objTuple.Item7);
        }

        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            c = Comparer<T6>.Default.Compare(Item6, other.Item6);
            if (c != 0) return c;

            return Comparer<T7>.Default.Compare(Item7, other.Item7);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            c = comparer.Compare(Item4, objTuple.Item4);
            if (c != 0) return c;

            c = comparer.Compare(Item5, objTuple.Item5);
            if (c != 0) return c;

            c = comparer.Compare(Item6, objTuple.Item6);
            if (c != 0) return c;

            return comparer.Compare(Item7, objTuple.Item7);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                                    comparer.GetHashCode(Item2!),
                                    comparer.GetHashCode(Item3!),
                                    comparer.GetHashCode(Item4!),
                                    comparer.GetHashCode(Item5!),
                                    comparer.GetHashCode(Item6!),
                                    comparer.GetHashCode(Item7!));
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4, Item5, Item6, Item7)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }
        string ITupleInternal.ToStringEnd()
        {
            return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ")";
        }

        int ITupleInternal.Size => 7;
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ValueTuple_8 { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System
{

    /// <summary>
    /// Represents an 8-tuple, or octuple, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    /// <typeparam name="T3">The type of the tuple's third component.</typeparam>
    /// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
    /// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
    /// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
    /// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
    /// <typeparam name="TRest">The type of the tuple's eighth component.</typeparam>
    [StructLayout(LayoutKind.Auto)]
    internal struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, ITupleInternal
        where TRest : struct
    {
        private static readonly EqualityComparer<T1> s_t1Comparer = EqualityComparer<T1>.Default;
        private static readonly EqualityComparer<T2> s_t2Comparer = EqualityComparer<T2>.Default;
        private static readonly EqualityComparer<T3> s_t3Comparer = EqualityComparer<T3>.Default;
        private static readonly EqualityComparer<T4> s_t4Comparer = EqualityComparer<T4>.Default;
        private static readonly EqualityComparer<T5> s_t5Comparer = EqualityComparer<T5>.Default;
        private static readonly EqualityComparer<T6> s_t6Comparer = EqualityComparer<T6>.Default;
        private static readonly EqualityComparer<T7> s_t7Comparer = EqualityComparer<T7>.Default;
        private static readonly EqualityComparer<TRest> s_tRestComparer = EqualityComparer<TRest>.Default;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's first component.
        /// </summary>
        public T1 Item1;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's second component.
        /// </summary>
        public T2 Item2;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's third component.
        /// </summary>
        public T3 Item3;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's seventh component.
        /// </summary>
        public T7 Item7;
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance's eighth component.
        /// </summary>
        public TRest Rest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        /// <param name="item7">The value of the tuple's seventh component.</param>
        /// <param name="rest">The value of the tuple's eight component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            if (!(rest is ITupleInternal))
            {
                throw new ArgumentException("The last element of an eight element ValueTuple must be a ValueTuple.");
            }

            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Rest = rest;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> && Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/>
        /// instance is equal to a specified <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
        {
            return s_t1Comparer.Equals(Item1, other.Item1)
                && s_t2Comparer.Equals(Item2, other.Item2)
                && s_t3Comparer.Equals(Item3, other.Item3)
                && s_t4Comparer.Equals(Item4, other.Item4)
                && s_t5Comparer.Equals(Item5, other.Item5)
                && s_t6Comparer.Equals(Item6, other.Item6)
                && s_t7Comparer.Equals(Item7, other.Item7)
                && s_tRestComparer.Equals(Rest, other.Rest);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)) return false;

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;

            return comparer.Equals(Item1, objTuple.Item1)
                && comparer.Equals(Item2, objTuple.Item2)
                && comparer.Equals(Item3, objTuple.Item3)
                && comparer.Equals(Item4, objTuple.Item4)
                && comparer.Equals(Item5, objTuple.Item5)
                && comparer.Equals(Item6, objTuple.Item6)
                && comparer.Equals(Item7, objTuple.Item7)
                && comparer.Equals(Rest, objTuple.Rest);
        }

        int IComparable.CompareTo(object other)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            return CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other);
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater 
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            c = Comparer<T6>.Default.Compare(Item6, other.Item6);
            if (c != 0) return c;

            c = Comparer<T7>.Default.Compare(Item7, other.Item7);
            if (c != 0) return c;

            return Comparer<TRest>.Default.Compare(Rest, other.Rest);
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (other == null) return 1;

            if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
            {
                throw new ArgumentException("Argument must be of type {0}.", nameof(other));
            }

            var objTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;

            int c = comparer.Compare(Item1, objTuple.Item1);
            if (c != 0) return c;

            c = comparer.Compare(Item2, objTuple.Item2);
            if (c != 0) return c;

            c = comparer.Compare(Item3, objTuple.Item3);
            if (c != 0) return c;

            c = comparer.Compare(Item4, objTuple.Item4);
            if (c != 0) return c;

            c = comparer.Compare(Item5, objTuple.Item5);
            if (c != 0) return c;

            c = comparer.Compare(Item6, objTuple.Item6);
            if (c != 0) return c;

            c = comparer.Compare(Item7, objTuple.Item7);
            if (c != 0) return c;

            return comparer.Compare(Rest, objTuple.Rest);
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            // We want to have a limited hash in this case.  We'll use the last 8 elements of the tuple
            ITupleInternal rest = Rest as ITupleInternal;
            if (rest == null)
            {
                return HashCode.Combine(s_t1Comparer.GetHashCode(Item1), s_t2Comparer.GetHashCode(Item2), s_t3Comparer.GetHashCode(Item3), s_t4Comparer.GetHashCode(Item4), s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7));
            }

            int size = rest.Size;
            if (size >= 8) { return rest.GetHashCode(); }

            // In this case, the rest member has less than 8 elements so we need to combine some our elements with the elements in rest
            int k = 8 - size;
            switch (k)
            {
                case 1:
                    return HashCode.Combine(s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 2:
                    return HashCode.Combine(s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 3:
                    return HashCode.Combine(s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 4:
                    return HashCode.Combine(s_t4Comparer.GetHashCode(Item4), s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 5:
                    return HashCode.Combine(s_t3Comparer.GetHashCode(Item3), s_t4Comparer.GetHashCode(Item4), s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 6:
                    return HashCode.Combine(s_t2Comparer.GetHashCode(Item2), s_t3Comparer.GetHashCode(Item3), s_t4Comparer.GetHashCode(Item4), s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
                case 7:
                case 8:
                    return HashCode.Combine(s_t1Comparer.GetHashCode(Item1), s_t2Comparer.GetHashCode(Item2), s_t3Comparer.GetHashCode(Item3), s_t4Comparer.GetHashCode(Item4), s_t5Comparer.GetHashCode(Item5), s_t6Comparer.GetHashCode(Item6), s_t7Comparer.GetHashCode(Item7), rest.GetHashCode());
            }

            Debug.Assert(false, "Missed all cases for computing ValueTuple hash code");
            return -1;
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            // We want to have a limited hash in this case.  We'll use the last 8 elements of the tuple
            ITupleInternal rest = Rest as ITupleInternal;
            if (rest == null)
            {
                return HashCode.Combine(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7));
            }

            int size = rest.Size;
            if (size >= 8) { return rest.GetHashCode(comparer); }

            // In this case, the rest member has less than 8 elements so we need to combine some our elements with the elements in rest
            int k = 8 - size;
            switch (k)
            {
                case 1:
                    return HashCode.Combine(comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 2:
                    return HashCode.Combine(comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 3:
                    return HashCode.Combine(comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 4:
                    return HashCode.Combine(comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 5:
                    return HashCode.Combine(comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 6:
                    return HashCode.Combine(comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
                case 7:
                case 8:
                    return HashCode.Combine(comparer.GetHashCode(Item1), comparer.GetHashCode(Item2), comparer.GetHashCode(Item3), comparer.GetHashCode(Item4), comparer.GetHashCode(Item5), comparer.GetHashCode(Item6), comparer.GetHashCode(Item7), rest.GetHashCode(comparer));
            }

            Debug.Assert(false, "Missed all cases for computing ValueTuple hash code");
            return -1;
        }

        int ITupleInternal.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Rest)</c>.
        /// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString()
        {
            ITupleInternal rest = Rest as ITupleInternal;
            if (rest == null)
            {
                return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ", " + Rest.ToString() + ")";
            }
            else
            {
                return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ", " + rest.ToStringEnd();
            }
        }

        string ITupleInternal.ToStringEnd()
        {
            ITupleInternal rest = Rest as ITupleInternal;
            if (rest == null)
            {
                return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ", " + Rest.ToString() + ")";
            }
            else
            {
                return Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " + Item7?.ToString() + ", " + rest.ToStringEnd();
            }
        }

        int ITupleInternal.Size
        {
            get
            {
                ITupleInternal rest = Rest as ITupleInternal;
                return rest == null ? 8 : 7 + rest.Size;
            }
        }
    }
}
"""""""""", Encoding.UTF8);
public static SourceText Source_T_System_ITupleInternal { get; } = SourceText.From(""""""""""
// <auto-generated/>
#pragma warning disable
#nullable enable annotations
// when T:System.ValueTuple
// when T:System.ValueTuple`1
// when T:System.ValueTuple`2
// when T:System.ValueTuple`3
// when T:System.ValueTuple`4
// when T:System.ValueTuple`5
// when T:System.ValueTuple`6
// when T:System.ValueTuple`7
// when T:System.ValueTuple`8
using System.Collections;

namespace System;
internal interface ITupleInternal
{
    int GetHashCode(IEqualityComparer comparer);
    int Size { get; }
    string ToStringEnd();
}

"""""""""", Encoding.UTF8);
}
