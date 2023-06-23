using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Meziantou.Polyfill.Generator;
internal sealed class PolyfillData
{
    public PolyfillData(string content) => Content = content;

    public string? Content { get; }

    public bool RequiresSpanOfT { get; set; }
    public bool RequiresReadOnlySpanOfT { get; set; }
    public bool RequiresMemory { get; set; }
    public bool RequiresReadOnlyMemory { get; set; }
    public bool RequiresValueTask { get; set; }
    public bool RequiresValueTaskOfT { get; set; }

    public static PolyfillData Get(string content)
    {
        var data = new PolyfillData(content);

        var tree = CSharpSyntaxTree.ParseText(content);
        var compilation = CSharpCompilation.Create("compilation",
          new[] { tree },
          Basic.Reference.Assemblies.Net80.References.All,
          new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var semanticModel = compilation.GetSemanticModel(tree);

        var root = tree.GetRoot();
        foreach (var type in root.DescendantNodes().OfType<TypeDeclarationSyntax>())
        {
            var symbol = (ITypeSymbol)semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.DeclaredAccessibility == Accessibility.Public)
                throw new Exception("All types must be internal");
        }

        var types = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(method)!;
            types.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
                types.Add(param);
        }

        foreach (var type in types)
        {
            data.RequiresSpanOfT |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.Span`1"));
            data.RequiresReadOnlySpanOfT |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.ReadOnlySpan`1"));
            data.RequiresMemory |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.Memory`1"));
            data.RequiresReadOnlyMemory |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.ReadOnlyMemory`1"));
            data.RequiresValueTask |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.Threading.Tasks.ValueTask"));
            data.RequiresValueTaskOfT |= SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName("System.Threading.Tasks.ValueTask`1"));
        }

        return data;
    }
}
