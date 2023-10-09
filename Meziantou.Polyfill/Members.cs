using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal partial struct Members
{
    private static bool IncludeMember(Compilation compilation, PolyfillOptions? options, string memberDocumentationId)
    {
        if (options != null && !options.Include(memberDocumentationId))
            return false;

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
}
