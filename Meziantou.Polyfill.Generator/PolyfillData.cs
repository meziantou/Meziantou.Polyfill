using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Meziantou.Polyfill.Generator;

internal sealed partial class PolyfillData
{
    private static readonly string[] PotentialRequiredTypes =
    [
        "System.Buffers.SpanAction`2",
        "System.Collections.Generic.IAsyncEnumerable`1",
        "System.Collections.Generic.IAsyncEnumerable`1",
        "System.Collections.Generic.IAsyncEnumerator`1",
        "System.Collections.Immutable.ImmutableArray`1",
        "System.DateOnly",
        "System.IAsyncDisposable",
        "System.IO.Compression.ZipArchiveEntry",
        "System.Memory`1",
        "System.Net.Http.HttpContent",
        "System.Net.Http.HttpMethod",
        "System.ReadOnlyMemory`1",
        "System.ReadOnlySpan`1",
        "System.Security.Cryptography.IncrementalHash",
        "System.Diagnostics.CodeAnalysis.StringSyntaxAttribute",
        "System.Runtime.CompilerServices.DefaultInterpolatedStringHandler",
        "System.Runtime.CompilerServices.Unsafe",
        "System.Span`1",
        "System.Text.Rune",
        "System.Threading.ITimer",
        "System.Threading.Tasks.Sources.IValueTaskSource`1",
        "System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags",
        "System.Threading.Tasks.Sources.ValueTaskSourceStatus",
        "System.Threading.Tasks.ValueTask",
        "System.Threading.Tasks.ValueTask`1",
        "System.TimeProvider",
        "System.TimeOnly",
    ];

    public PolyfillData(string content) => Content = content;

    public string? Content { get; }

    public string? XmlDocumentationId { get; private set; }

    public HashSet<string> RequiredTypes { get; private set; } = new HashSet<string>(StringComparer.Ordinal);
    public string[] DeclaredMemberDocumentationIds { get; private set; } = [];
    public string[] DeclaredMethodDocumentationIds { get; private set; } = [];
    public string[] DeclaredPropertyDocumentationIds { get; private set; } = [];
    public (string DocumentationId, ISymbol Symbol)[] DeclaredMethodSymbols { get; private set; } = [];
    public (string DocumentationId, ISymbol Symbol)[] DeclaredPropertySymbols { get; private set; } = [];
    public string[] ConditionalMembers { get; private set; } = [];
    public string[] TypeDefines { get; private set; } = [];
    public string[] ConditionalSymbols { get; private set; } = [];
    public string[] PolyfillExtensionsClassNames { get; private set; } = [];

    public bool UseUnsafe { get; private set; }
    public bool UseExtensions { get; private set; }

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
        documentationDeclarationId = GetXmlDocId(content) ?? documentationDeclarationId;
        foreach (var type in root.DescendantNodes(descendIntoChildren: node => node is not TypeDeclarationSyntax).OfType<TypeDeclarationSyntax>())
        {
            var symbol = (ITypeSymbol)semanticModel.GetDeclaredSymbol(type)!;
            if (symbol.DeclaredAccessibility == Accessibility.Public)
                throw new InvalidOperationException("The symbol " + symbol.ToDisplayString() + " must be internal");
        }

        var requiredTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        var declaredMembers = new HashSet<string>(StringComparer.Ordinal);
        var declaredMethodSymbols = new Dictionary<string, ISymbol>(StringComparer.Ordinal);
        var declaredPropertySymbols = new Dictionary<string, ISymbol>(StringComparer.Ordinal);
        var polyfillExtensionsClassNames = new HashSet<string>(StringComparer.Ordinal);

        // Collect all class names starting with "PolyfillExtensions"
        foreach (var type in root.DescendantNodes(descendIntoChildren: node => node is not TypeDeclarationSyntax).OfType<ClassDeclarationSyntax>())
        {
            if (type.Identifier.ValueText.StartsWith("PolyfillExtensions", StringComparison.Ordinal))
            {
                polyfillExtensionsClassNames.Add(type.Identifier.ValueText);
            }
        }

        foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(method)!;
            requiredTypes.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }

            AddReadmeMember(symbol, declaredMethodSymbols);

            if (IsExposedForDeclaredMemberTable(symbol))
            {
                var declarationId = DocumentationCommentId.CreateDeclarationId(symbol);
                if (declarationId is not null)
                {
                    declaredMembers.Add(declarationId);
                }
            }
        }

        foreach (var constructor in root.DescendantNodes().OfType<ConstructorDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(constructor)!;
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }
        }

        foreach (var @operator in root.DescendantNodes().OfType<OperatorDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(@operator)!;
            requiredTypes.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }

            AddReadmeMember(symbol, declaredMethodSymbols);
        }

        foreach (var conversionOperator in root.DescendantNodes().OfType<ConversionOperatorDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(conversionOperator)!;
            requiredTypes.Add(symbol.ReturnType);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }

            AddReadmeMember(symbol, declaredMethodSymbols);
        }

        foreach (var property in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(property)!;
            requiredTypes.Add(symbol.Type);
            AddReadmeMember(symbol, declaredPropertySymbols);
        }

        foreach (var indexer in root.DescendantNodes().OfType<IndexerDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(indexer)!;
            requiredTypes.Add(symbol.Type);
            foreach (var param in symbol.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
            }

            AddReadmeMember(symbol, declaredPropertySymbols);
        }

        foreach (var field in root.DescendantNodes().OfType<FieldDeclarationSyntax>())
        {
            var type = semanticModel.GetTypeInfo(field.Declaration.Type).Type;
            if (type is not null)
            {
                requiredTypes.Add(type);
            }

            foreach (var variable in field.Declaration.Variables)
            {
                var symbol = semanticModel.GetDeclaredSymbol(variable);
                if (symbol is not null)
                {
                    AddReadmeMember(symbol, declaredPropertySymbols);
                }
            }
        }

        foreach (var @delegate in root.DescendantNodes().OfType<DelegateDeclarationSyntax>())
        {
            var symbol = semanticModel.GetDeclaredSymbol(@delegate)!;
            var invokeMethod = symbol.DelegateInvokeMethod;
            if (invokeMethod is null)
                continue;

            requiredTypes.Add(invokeMethod.ReturnType);
            foreach (var param in invokeMethod.Parameters.Select(p => p.Type))
            {
                requiredTypes.Add(param);
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

        foreach (var name in root.DescendantNodes().OfType<NameSyntax>())
        {
            if (semanticModel.GetSymbolInfo(name).Symbol is ITypeSymbol type)
            {
                requiredTypes.Add(type);
            }
        }

        var useExtensions = root.DescendantNodes().OfType<ExtensionBlockDeclarationSyntax>().Any();
        var useUnsafe = root.DescendantNodes().OfType<UnsafeStatementSyntax>().Any() || root.DescendantNodes().OfType<MethodDeclarationSyntax>().Any(m => m.Modifiers.Any(m => m.IsKind(SyntaxKind.UnsafeKeyword)));

        var finalContent = new AddEmbeddedAttributeRewriter().Visit(root) ?? root;

        var data = new PolyfillData(finalContent.ToFullString());
        data.ConditionalMembers = GetConditions(content);
        data.TypeDefines = GetTypeDefines(content);
        data.XmlDocumentationId = documentationDeclarationId;
        data.DeclaredMethodDocumentationIds = [.. declaredMethodSymbols.Keys.Order(StringComparer.Ordinal)];
        data.DeclaredPropertyDocumentationIds = [.. declaredPropertySymbols.Keys.Order(StringComparer.Ordinal)];
        data.DeclaredMethodSymbols =
        [
            .. declaredMethodSymbols
                .Where(item => item.Value is not IMethodSymbol { MethodKind: MethodKind.Constructor or MethodKind.StaticConstructor })
                .OrderBy(item => item.Key, StringComparer.Ordinal)
                .Select(item => (item.Key, item.Value)),
        ];
        data.DeclaredPropertySymbols = [.. declaredPropertySymbols.OrderBy(item => item.Key, StringComparer.Ordinal).Select(item => (item.Key, item.Value))];
        data.DeclaredMemberDocumentationIds = [.. declaredMembers];
        data.PolyfillExtensionsClassNames = [.. polyfillExtensionsClassNames.OrderBy(x => x, StringComparer.Ordinal)];
        data.UseExtensions = useExtensions;
        data.UseUnsafe = useUnsafe;

        foreach (var requiredType in requiredTypes)
        {
            foreach (var potentialRequiredType in PotentialRequiredTypes)
            {
                if (data.XmlDocumentationId == "T:" + potentialRequiredType)
                    continue;

                if (SymbolEqualityComparer.Default.Equals(requiredType.OriginalDefinition, compilation.GetTypeByMetadataName(potentialRequiredType)))
                {
                    data.RequiredTypes.Add(potentialRequiredType);
                }
            }
        }

        return data;

        void AddReadmeMember(ISymbol symbol, Dictionary<string, ISymbol> declaredSymbols)
        {
            if (!IsExposedReadmeMember(symbol))
                return;

            if (!IsDeclaredOnDocumentedType(symbol))
                return;

            var declarationId = DocumentationCommentId.CreateDeclarationId(symbol);
            if (declarationId is not null)
            {
                declaredSymbols.TryAdd(declarationId, symbol);
            }
        }

        bool IsDeclaredOnDocumentedType(ISymbol symbol)
        {
            if (!documentationDeclarationId.StartsWith("T:", StringComparison.Ordinal))
                return true;

            var containingType = symbol.ContainingType;
            if (containingType is null)
                return false;

            return string.Equals(DocumentationCommentId.CreateDeclarationId(containingType), documentationDeclarationId, StringComparison.Ordinal);
        }

        static bool IsExposedReadmeMember(ISymbol symbol)
        {
            if (symbol is INamespaceSymbol or IAssemblySymbol)
                return true;

            if (symbol.DeclaredAccessibility is not Accessibility.Public and not Accessibility.Internal)
                return false;

            if (symbol.ContainingSymbol is null)
                return true;

            return IsExposedReadmeMember(symbol.ContainingSymbol);
        }

        static bool IsExposedForDeclaredMemberTable(ISymbol symbol)
        {
            if (symbol.DeclaredAccessibility is not Accessibility.Public and not Accessibility.Internal)
                return false;

            if (symbol.ContainingSymbol is null)
                return true;

            return IsExposedForDeclaredMemberTable(symbol.ContainingSymbol);
        }

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

        static string[] GetTypeDefines(string content)
        {
            return [.. TypeDefineRegex().Matches(content.ReplaceLineEndings("\n")).Cast<Match>().Select(m => m.Groups["type"].Value).Order(StringComparer.Ordinal)];
        }
    }

    [GeneratedRegex("""^//\s*when\s+(?<member>[^\s]+)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline, matchTimeoutMilliseconds: -1)]
    private static partial Regex ConditionRegex();

    [GeneratedRegex("""^//\s*XML-DOC:\s+(?<value>.*)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline, matchTimeoutMilliseconds: -1)]
    private static partial Regex XmlDocRegex();

    [GeneratedRegex("""^//\s*define-type\s+(?<type>[^\s]+)$""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Multiline, matchTimeoutMilliseconds: -1)]
    private static partial Regex TypeDefineRegex();

    internal sealed class AddEmbeddedAttributeRewriter : CSharpSyntaxRewriter
    {
        private static SyntaxList<AttributeListSyntax> AddGeneratedAttributes(SyntaxList<AttributeListSyntax> existingAttributeLists)
        {
            return existingAttributeLists
                // [EmbeddedAttribute]
                .Add(SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(SyntaxFactory.ParseName("Microsoft.CodeAnalysis.EmbeddedAttribute"))))
                    .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia("#if !MEZIANTOU_POLYFILL_SKIP_MICROSOFT_CODEANALYSIS_EMBEDDEDATTRIBUTE\n"))
                    .WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n")))
                // [GeneratedCodeAttribute]
                .Add(SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(
                            SyntaxFactory.ParseName("System.CodeDom.Compiler.GeneratedCodeAttribute"),
                            SyntaxFactory.ParseAttributeArgumentList("(\"Meziantou.Polyfill\", \"\")"))))
                    .WithLeadingTrivia(SyntaxFactory.ParseLeadingTrivia("#endif\n"))
                    .WithTrailingTrivia(SyntaxFactory.ParseTrailingTrivia("\n")));
        }

        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // Skip classes that start with "PolyfillExtensions" - they will be handled separately
            if (node.Identifier.ValueText.StartsWith("PolyfillExtensions", StringComparison.Ordinal))
                return node;

            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }

        public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
        {
            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }

        public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }

        public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
        {
            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }

        public override SyntaxNode? VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }

        public override SyntaxNode? VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            return node.WithAttributeLists(AddGeneratedAttributes(node.AttributeLists));
        }
    }
}
