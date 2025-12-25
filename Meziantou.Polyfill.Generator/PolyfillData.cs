using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Meziantou.Polyfill.Generator;

internal sealed partial class PolyfillData
{
    private static readonly string[] PotentialRequiredTypes =
    [
        "System.Collections.Generic.IAsyncEnumerable`1",
        "System.Span`1",
        "System.ReadOnlySpan`1",
        "System.Memory`1",
        "System.ReadOnlyMemory`1",
        "System.Threading.Tasks.ValueTask",
        "System.Threading.Tasks.ValueTask`1",
        "System.Collections.Immutable.ImmutableArray`1",
        "System.Net.Http.HttpMethod",
        "System.Net.Http.HttpContent",
        "System.IAsyncDisposable",
        "System.Collections.Generic.IAsyncEnumerable`1",
        "System.Collections.Generic.IAsyncEnumerator`1",
        "System.IO.Compression.ZipArchiveEntry",
        "System.Runtime.CompilerServices.DefaultInterpolatedStringHandler",
    ];

    public PolyfillData(string content) => Content = content;

    public string? Content { get; }

    public string? XmlDocumentationId { get; private set; }

    public HashSet<string> RequiredTypes { get; private set; } = new HashSet<string>(StringComparer.Ordinal);
    public string[] DeclaredMemberDocumentationIds { get; private set; } = [];
    public string[] ConditionalMembers { get; private set; } = [];
    public string[] ConditionalSymbols { get; private set; } = [];

    public bool UseUnsafe { get; private set; }
    public bool UseExtensions { get; private set; }
    public bool SupportInternalsVisibleTo { get; private set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"XmlDocumentationId: {XmlDocumentationId}");
        sb.AppendLine($"RequiredTypes: {string.Join(", ", RequiredTypes)}");
        sb.AppendLine($"DeclaredMemberDocumentationIds: {string.Join(", ", DeclaredMemberDocumentationIds)}");
        sb.AppendLine($"ConditionalMembers: {string.Join(", ", ConditionalMembers)}");
        sb.AppendLine($"Content:\n{Content}");
        return sb.ToString();
    }

    public static PolyfillData Get(CSharpCompilation compilation, string documentationDeclarationId, string content)
    {
        var tree = CSharpSyntaxTree.ParseText(content);
        compilation = compilation.AddSyntaxTrees(tree);

        var semanticModel = compilation.GetSemanticModel(tree);

        var root = tree.GetRoot();
        foreach (var type in root.DescendantNodes(descendIntoChildren: node => node is not TypeDeclarationSyntax).OfType<TypeDeclarationSyntax>())
        {
            var symbol = (ITypeSymbol)semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.DeclaredAccessibility == Accessibility.Public)
                throw new InvalidOperationException("The symbol " + symbol.ToDisplayString() + " must be internal");
        }

        var requiredTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        var declaredMethods = new HashSet<string>(StringComparer.Ordinal);

        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(method)!;
            requiredTypes.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }

            if (IsExposed(symbol))
            {
                var declarationId = DocumentationCommentId.CreateDeclarationId(symbol);
                if (declarationId is not null)
                {
                    declaredMethods.Add(declarationId);
                }
            }

            static bool IsExposed(ISymbol symbol)
            {
                if (symbol.DeclaredAccessibility is not Accessibility.Public and not Accessibility.Internal)
                    return false;

                if (symbol.ContainingSymbol is null)
                    return true;

                return IsExposed(symbol.ContainingSymbol);
            }
        }

        foreach (var type in root.DescendantNodes().OfType<TypeDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.BaseType != null)
            {
                requiredTypes.Add(symbol.BaseType);
            }

            foreach (var iface in symbol.AllInterfaces)
            {
                requiredTypes.Add(iface);
            }
        }

        foreach (var extensionBlock in root.DescendantNodes().OfType<ExtensionBlockDeclarationSyntax>())
        {
            foreach (var parameter in extensionBlock.ParameterList!.Parameters)
            {
                var parameterType = semanticModel.GetTypeInfo(parameter.Type!).Type!;
                requiredTypes.Add(parameterType);
            }
        }

        documentationDeclarationId = GetXmlDocId(content) ?? documentationDeclarationId;
        var useExtensions = root.DescendantNodes().OfType<ExtensionBlockDeclarationSyntax>().Any();
        var useUnsafe = root.DescendantNodes().OfType<UnsafeStatementSyntax>().Any() || root.DescendantNodes().OfType<MethodDeclarationSyntax>().Any(m => m.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)));

        var supportInternalsVisibleTo = documentationDeclarationId.StartsWith("T:", StringComparison.Ordinal);
        var finalContent = supportInternalsVisibleTo ? root : new AddEmbeddedAttributeRewriter().Visit(root);

        var data = new PolyfillData(finalContent.ToFullString());
        data.ConditionalMembers = GetConditions(content);
        data.XmlDocumentationId = documentationDeclarationId;
        data.DeclaredMemberDocumentationIds = [.. declaredMethods];
        data.UseExtensions = useExtensions;
        data.UseUnsafe = useUnsafe;
        data.SupportInternalsVisibleTo = supportInternalsVisibleTo;

        foreach (var requiredType in requiredTypes)
        {
            foreach (var potentialRequiredType in PotentialRequiredTypes)
            {
                if (SymbolEqualityComparer.Default.Equals(requiredType.OriginalDefinition, compilation.GetTypeByMetadataName(potentialRequiredType)))
                {
                    data.RequiredTypes.Add(potentialRequiredType);
                }
            }
        }

        return data;

        static string[] GetConditions(string content)
        {
            return [.. ConditionRegex().Matches(content.ReplaceLineEndings("\n")).Cast<Match>().Select(m => m.Groups["member"].Value).Order(StringComparer.Ordinal)];
        }

        static string? GetXmlDocId(string content)
        {
            var match = XmlDocRegex().Match(content);
            if (match.Success)
                return match.Groups["value"].Value.Trim();

            return null;
        }
    }

    [GeneratedRegex("""^//\s*when\s+(?<member>[^\s]+)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline, matchTimeoutMilliseconds: -1)]
    private static partial Regex ConditionRegex();

    [GeneratedRegex("""^//\s*XML-DOC:\s+(?<value>.*)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline, matchTimeoutMilliseconds: -1)]
    private static partial Regex XmlDocRegex();

    internal sealed class AddEmbeddedAttributeRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Identifier.ValueText == "PolyfillExtensions")
                return node;

            return node.WithAttributeLists(node.AttributeLists.Add(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute")))).WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n"))));
        }
        public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
        {
            return node.WithAttributeLists(node.AttributeLists.Add(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute")))).WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n"))));

        }

        public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            return node.WithAttributeLists(node.AttributeLists.Add(
                       SyntaxFactory.AttributeList(
                           SyntaxFactory.SingletonSeparatedList(
                               SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute")))).WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n"))));
        }

        public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
        {
            return node.WithAttributeLists(node.AttributeLists.Add(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute")))).WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n"))));
        }

        public override SyntaxNode? VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            return node.WithAttributeLists(node.AttributeLists.Add(
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute")))).WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n"))));
        }
    }
}
