using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal partial struct Members
{
    private static bool IncludeMember(IncludeContext context, string memberDocumentationId)
    {
        if (context.Options is not null && !context.Options.Include(memberDocumentationId))
            return false;

        var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(memberDocumentationId, context.Compilation);
        foreach (var symbol in symbols)
        {
            if (ReferenceEquals(symbol.ContainingAssembly, context.Compilation.Assembly))
                return false;

            // IsEmbeddedSymbol is a workaround for https://github.com/dotnet/roslyn/issues/79498
            if (context.Compilation.IsSymbolAccessibleWithin(symbol, context.Compilation.Assembly) && !IsEmbeddedSymbol(symbol))
                return false;
        }

        return true;
    }

    private static bool IsEmbeddedSymbol(ISymbol symbol)
    {
        if(symbol is not ITypeSymbol)
        {
            symbol = symbol.ContainingType;
        }

        if (symbol is not ITypeSymbol)
            return false;

        var attributeSymbol = symbol.ContainingAssembly.GetTypeByMetadataName("Microsoft.CodeAnalysis.EmbeddedAttribute");
        if(attributeSymbol is null)
            return false;

        var attributes = symbol.GetAttributes();
        foreach (var attribute in attributes)
        {
            if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, attributeSymbol))
                return true;
        }

        return false;
    }

    private readonly struct IncludeContext(Compilation compilation, PolyfillOptions? options)
    {
        public Compilation Compilation { get; } = compilation;
        public PolyfillOptions? Options { get; } = options;
    }
}
