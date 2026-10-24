using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal partial struct Members
{
    private static bool IncludeMember(IncludeContext context, string memberDocumentationId)
        => GetMemberSkipReason(context, memberDocumentationId) is null;

    private static string? GetMemberSkipReason(IncludeContext context, string memberDocumentationId, string? parameterRefKinds = null)
    {
        var optionSkipReason = context.Options?.GetSkipReason(memberDocumentationId);
        if (optionSkipReason is not null)
            return optionSkipReason;

        var compilation = context.Compilation;
        var currentAssembly = compilation.Assembly;
        var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(memberDocumentationId, compilation);
        IAssemblySymbol? accessibleFromAssembly = null;
        foreach (var symbol in symbols)
        {
            // 'ref', 'in' and 'out' share the same documentation ID (e.g. MemoryMarshal.Write(Span<byte>, ref T)
            // in System.Memory vs MemoryMarshal.Write(Span<byte>, in T) in .NET 8), but are not source-compatible.
            if (parameterRefKinds is not null && !HasParameterRefKinds(symbol, parameterRefKinds))
                continue;

            if (ReferenceEquals(symbol.ContainingAssembly, currentAssembly))
                return "already available in compilation";

            // IsEmbeddedSymbol is a workaround for https://github.com/dotnet/roslyn/issues/79498
            if (compilation.IsSymbolAccessibleWithin(symbol, currentAssembly) && !context.IsEmbeddedSymbol(symbol))
            {
                if (accessibleFromAssembly is null)
                {
                    accessibleFromAssembly = symbol.ContainingAssembly;
                }
                else if (!ReferenceEquals(accessibleFromAssembly, symbol.ContainingAssembly))
                {
                    // The symbol is accessible from multiple assemblies, which would cause
                    // ambiguity (e.g. CS0433). Generate a local copy so the compiler prefers
                    // the current assembly's definition.
                    return null;
                }
            }
        }

        if (accessibleFromAssembly is not null)
            return "already available in compilation";

        return null;
    }

    // Must stay in sync with GetParameterRefKinds in Meziantou.Polyfill.Generator/Program.cs
    private static bool HasParameterRefKinds(ISymbol symbol, string parameterRefKinds)
    {
        var parameters = symbol switch
        {
            IMethodSymbol method => method.Parameters,
            IPropertySymbol property => property.Parameters,
            _ => default,
        };

        if (parameters.IsDefault || parameters.Length != parameterRefKinds.Length)
            return false;

        for (var i = 0; i < parameters.Length; i++)
        {
            var expected = (int)parameters[i].RefKind switch
            {
                0 => '-', // None
                1 => 'r', // Ref
                2 => 'o', // Out
                3 or 4 => 'i', // In, RefReadOnlyParameter (not available in the Roslyn version referenced by this project)
                _ => '?',
            };

            if (expected != parameterRefKinds[i])
                return false;
        }

        return true;
    }

    private void AddSource(SourceProductionContext context, string path, string content)
    {
        context.AddSource(path, SourceText.From(_sourcePrefix + content, Encoding.UTF8));
    }

    private sealed class IncludeContext
    {
        private readonly Dictionary<IAssemblySymbol, INamedTypeSymbol?> _embeddedAttributeCache
            = new(SymbolEqualityComparer.Default);

        private readonly Dictionary<ITypeSymbol, bool> _isEmbeddedTypeCache
            = new(SymbolEqualityComparer.Default);

        public IncludeContext(Compilation compilation, PolyfillOptions? options)
        {
            Compilation = compilation;
            Options = options;
        }

        public Compilation Compilation { get; }
        public PolyfillOptions? Options { get; }

        public bool IsSymbolAccessible(ISymbol symbol)
        {
            if (!Compilation.IsSymbolAccessibleWithin(symbol, Compilation.Assembly))
                return false;

            var type = symbol as ITypeSymbol;
            if (symbol is not ITypeSymbol)
            {
                type = symbol.ContainingType;
            }

            if (type is not null)
                return !IsEmbeddedType(type);

            return true;
        }

        public bool IsEmbeddedSymbol(ISymbol symbol)
        {
            if (symbol is ITypeSymbol typeSymbol)
                return IsEmbeddedType(typeSymbol);

            if (symbol.ContainingType is not null)
                return IsEmbeddedType(symbol.ContainingType);

            return false;
        }

        public bool IsEmbeddedType(ITypeSymbol typeSymbol)
        {
            if (_isEmbeddedTypeCache.TryGetValue(typeSymbol, out var cached))
                return cached;

            var result = ComputeIsEmbeddedType(typeSymbol);
            _isEmbeddedTypeCache[typeSymbol] = result;
            return result;
        }

        private bool ComputeIsEmbeddedType(ITypeSymbol typeSymbol)
        {
            var attributeSymbol = GetEmbeddedAttribute(typeSymbol.ContainingAssembly);
            if (attributeSymbol is null)
                return false;

            foreach (var attribute in typeSymbol.GetAttributes())
            {
                if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, attributeSymbol))
                    return true;
            }

            return false;
        }

        private INamedTypeSymbol? GetEmbeddedAttribute(IAssemblySymbol assembly)
        {
            if (_embeddedAttributeCache.TryGetValue(assembly, out var cached))
                return cached;

            var resolved = assembly.GetTypeByMetadataName("Microsoft.CodeAnalysis.EmbeddedAttribute");
            _embeddedAttributeCache[assembly] = resolved;
            return resolved;
        }
    }
}
