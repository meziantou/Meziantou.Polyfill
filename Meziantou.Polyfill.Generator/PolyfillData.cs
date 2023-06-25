using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace Meziantou.Polyfill.Generator;
internal sealed partial class PolyfillData
{
    public PolyfillData(string content) => Content = content;

    public string? Content { get; }

    public bool RequiresSpanOfT { get; private set; }
    public bool RequiresReadOnlySpanOfT { get; private set; }
    public bool RequiresMemory { get; private set; }
    public bool RequiresReadOnlyMemory { get; private set; }
    public bool RequiresValueTask { get; private set; }
    public bool RequiresValueTaskOfT { get; private set; }

    public string[] DeclaredMemberDocumentationIds { get; private set; } = Array.Empty<string>();
    public string[] ConditionalMembers { get; private set; } = Array.Empty<string>();

    public static PolyfillData Get(string content)
    {
        var data = new PolyfillData(content);
        data.ConditionalMembers = GetConditions(content);

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
        var declaredMethods = new HashSet<string>(StringComparer.Ordinal);
        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(method)!;
            types.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
                types.Add(param);

            if (symbol.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
            {
                declaredMethods.Add(DocumentationCommentId.CreateDeclarationId(symbol));
            }
        }

        data.DeclaredMemberDocumentationIds = declaredMethods.ToArray();

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

        static string[] GetConditions(string content)
        {
            return ConditionRegex().Matches(content.ReplaceLineEndings("\n")).Cast<Match>().Select(m => m.Groups["member"].Value).Order().ToArray();
        }
    }

    [GeneratedRegex("""^//\s*when\s+(?<member>[^\s]+)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline)]
    private static partial Regex ConditionRegex();
}
