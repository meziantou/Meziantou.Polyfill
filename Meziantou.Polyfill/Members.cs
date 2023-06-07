using System;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Meziantou.Polyfill;

partial class Members
{
    private static bool IncludeMember(Compilation compilation, PolyfillOptions options, string memberDocumentationId)
    {
        if (!string.IsNullOrEmpty(options.IncludedPolyfills))
        {
            var found = false;
            foreach (var entry in new LineSplitEnumerator(options.IncludedPolyfills.AsSpan()))
            {
                if (entry.IsEmpty)
                    continue;

                if (memberDocumentationId.AsSpan().StartsWith(entry, StringComparison.Ordinal))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        if (memberDocumentationId == "T:System.Range")
        {
            if (compilation.GetTypeByMetadataName("System.ValueTuple`2") is null)
                return false;
        }
        else if (memberDocumentationId.StartsWith("M:", StringComparison.Ordinal))
        {
            if(memberDocumentationId.IndexOf("System.Span{", StringComparison.Ordinal) != -1)
            {
                if (compilation.GetTypeByMetadataName("System.Span`1") is null)
                    return false;
            }
            else if (memberDocumentationId.IndexOf("System.ReadOnlySpan{", StringComparison.Ordinal) != -1)
            {
                if (compilation.GetTypeByMetadataName("System.ReadOnlySpan`1") is null)
                    return false;
            } 
            else if(memberDocumentationId.IndexOf("System.Memory{", StringComparison.Ordinal) != -1)
            {
                if (compilation.GetTypeByMetadataName("System.Memory`1") is null)
                    return false;
            }
            else if (memberDocumentationId.IndexOf("System.ReadOnlyMemory{", StringComparison.Ordinal) != -1)
            {
                if (compilation.GetTypeByMetadataName("System.ReadOnlyMemory`1") is null)
                    return false;
            }
        }

        var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(memberDocumentationId, compilation);
        foreach (var symbol in symbols)
        {
            if (ReferenceEquals(symbol.ContainingAssembly, compilation.Assembly))
                return false;

            if (compilation.IsSymbolAccessibleWithin(symbol, compilation.Assembly))
                return false;
        }

        return true;
    }

    [StructLayout(LayoutKind.Auto)]
    private ref struct LineSplitEnumerator
    {
        private ReadOnlySpan<char> _str;

        public LineSplitEnumerator(ReadOnlySpan<char> str)
        {
            _str = str;
            Current = default;
        }

        public readonly LineSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str;
            if (span.IsEmpty)
                return false;

            var index = span.IndexOf(';');
            if (index == -1)
            {
                _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                Current = span.Trim();
                return true;
            }

            Current = span.Slice(0, index).Trim();
            _str = span.Slice(index + 1);
            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }
    }
}
