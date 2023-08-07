using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace Meziantou.Polyfill.Generator;
internal sealed partial class PolyfillData
{
    private static readonly string[] PotentialRequiredTypes =
    {
        "System.Span`1",
        "System.ReadOnlySpan`1",
        "System.Memory`1",
        "System.ReadOnlyMemory`1",
        "System.Threading.Tasks.ValueTask",
        "System.Threading.Tasks.ValueTask`1",
        "System.Collections.Immutable.ImmutableArray`1",
        "System.Net.Http.HttpContent",
    };

    public PolyfillData(string content) => Content = content;

    public string? Content { get; }

    public HashSet<string> RequiredTypes { get; private set; } = new HashSet<string>(StringComparer.Ordinal);
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
        foreach (var type in root.DescendantNodes(descendIntoChildren: node => node is not TypeDeclarationSyntax).OfType<TypeDeclarationSyntax>())
        {
            var symbol = (ITypeSymbol)semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.DeclaredAccessibility == Accessibility.Public)
                throw new Exception("The symbol " + symbol.ToDisplayString() + " must be internal");
        }

        var types = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        var declaredMethods = new HashSet<string>(StringComparer.Ordinal);

        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(method)!;
            types.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                types.Add(param);
            }

            if (symbol.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
            {
                declaredMethods.Add(DocumentationCommentId.CreateDeclarationId(symbol));
            }
        }

        foreach (var type in root.DescendantNodes().OfType<TypeDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.BaseType != null)
            {
                types.Add(symbol.BaseType);
            }

            foreach (var iface in symbol.AllInterfaces)
            {
                types.Add(iface);
            }
        }

        data.DeclaredMemberDocumentationIds = declaredMethods.ToArray();

        foreach (var type in types)
        {
            foreach (var requiredTypes in PotentialRequiredTypes)
            {
                if (SymbolEqualityComparer.Default.Equals(type.OriginalDefinition, compilation.GetTypeByMetadataName(requiredTypes)))
                {
                    data.RequiredTypes.Add(requiredTypes);
                }
            }
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
