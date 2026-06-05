using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal partial struct Members
{
    private static bool IncludeMember(IncludeContext context, string memberDocumentationId)
    {
        if (context.Options is not null && !context.Options.Include(memberDocumentationId))
            return false;

        return IsMemberMissing(context, memberDocumentationId);
    }

    private static bool IsMemberMissing(IncludeContext context, string memberDocumentationId)
    {
        var compilation = context.Compilation;
        var currentAssembly = compilation.Assembly;
        var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(memberDocumentationId, compilation);
        IAssemblySymbol? accessibleFromAssembly = null;
        foreach (var symbol in symbols)
        {
            if (ReferenceEquals(symbol.ContainingAssembly, currentAssembly))
                return false;

            // IsEmbeddedSymbol is a workaround for https://github.com/dotnet/roslyn/issues/79498
            if (compilation.IsSymbolAccessibleWithin(symbol, currentAssembly) && !IsEmbeddedSymbol(context, symbol))
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
                    return true;
                }
            }
        }

        if (accessibleFromAssembly is not null)
            return false;

        return true;
    }

    private static bool IsEmbeddedSymbol(IncludeContext context, ISymbol symbol)
    {
        if (symbol is not ITypeSymbol typeSymbol)
        {
            typeSymbol = symbol.ContainingType;
            if (typeSymbol is null)
                return false;
        }

        return context.IsEmbeddedType(typeSymbol);
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
