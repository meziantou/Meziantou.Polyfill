﻿using System.Reflection;
using System.Text;
using Meziantou.Polyfill.Generator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Meziantou.Framework;
using System.Text.RegularExpressions;

// Reference doesn't contains internal types which may be needed
// So, use the runtime types to get all available types and methods
var dotnetPath = FullPath.FromPath(typeof(object).Assembly.Location).Parent;
var files = Directory.GetFiles(dotnetPath);
var compilation = CSharpCompilation.Create(
    assemblyName: "compilation",
    references: files.Select(file => MetadataReference.CreateFromFile(file)),
    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, metadataImportOptions: MetadataImportOptions.All));

var assembly = Assembly.GetExecutingAssembly();
var polyfills = assembly.GetManifestResourceNames()
      .OrderBy(_ => _, StringComparer.Ordinal)
      .Select((item, index) =>
      {
          var typeName = Path.GetFileNameWithoutExtension(item).Replace(';', ':');
          var symbol = DocumentationCommentId.GetSymbolsForDeclarationId(typeName, compilation).Single();
          return new Polyfill()
          {
              Index = index,
              TypeName = typeName,
              Symbol = symbol,
              Kind = symbol.Kind switch
              {
                  SymbolKind.NamedType => PolyfillKind.Type,
                  SymbolKind.Method => PolyfillKind.Method,
                  _ => throw new Exception($"Unknown symbol kind '{symbol.Kind}'"),
              },
              OutputPath = Path.GetFileNameWithoutExtension(item)
                               .Replace(';', '_')
                               .Replace('@', '_') + ".g.cs",
              CSharpName = Path.GetFileNameWithoutExtension(item)
                               .Replace(';', '_')
                               .Replace('@', '_')
                               .Replace('{', '_')
                               .Replace('}', '_')
                               .Replace('(', '_')
                               .Replace(')', '_')
                               .Replace('[', '_')
                               .Replace(']', '_')
                               .Replace(',', '_')
                               .Replace('.', '_')
                               .Replace('`', '_'),
              PolyfillData = PolyfillData.Get(compilation, $$""""
                  // <auto-generated/>
                  #pragma warning disable
                  #nullable enable annotations
                  {{ReadResourceAsString(item)}}
                  """"),
          };
      })
      .ToArray();

polyfills = SortPolyfills(polyfills);

var requiredTypes = polyfills.SelectMany(p => p.PolyfillData.RequiredTypes)
    .Distinct(StringComparer.Ordinal)
    .Order(StringComparer.Ordinal)
    .Select(p => new
    {
        TypeName = p,
        CsharpFieldName = "_" + p.Replace('`', '_').Replace('.', '_')
    })
    .ToArray();

await GenerateMembers();
await GenerateReadme();

async Task GenerateMembers()
{
    var fieldCount = (polyfills.Length / 64) + (polyfills.Length % 64 > 0 ? 1 : 0);

    var sb = new StringBuilder();
    sb.AppendLine($$"""
    #nullable enable
    using System;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

    namespace Meziantou.Polyfill;

    internal readonly partial struct Members : IEquatable<Members>
    {
    """);

    for (var i = 0; i < fieldCount; i++)
    {
        sb.AppendLine($"private readonly ulong _bits{i} = 0uL;");
    }

    sb.AppendLine($"private readonly PolyfillOptions _options;");

    foreach (var requiredType in requiredTypes)
    {
        sb.AppendLine($"private readonly bool {requiredType.CsharpFieldName};");
    }

    sb.AppendLine("public Members(Compilation compilation, PolyfillOptions options)");
    sb.AppendLine("{");
    sb.AppendLine("    _options = options;");

    foreach (var requiredType in requiredTypes)
    {
        sb.AppendLine($"    {requiredType.CsharpFieldName} = compilation.GetTypeByMetadataName(\"{requiredType.TypeName}\") != null;");
    }

    foreach (var polyfill in polyfills)
    {
        sb.AppendLine($"    if ({GenerateIncludePreCondition(polyfill.PolyfillData)}IncludeMember(compilation, options, \"{polyfill.TypeName}\"){GenerateIncludePostCondition(polyfill.PolyfillData)})");
        sb.AppendLine($"        {polyfill.CSharpFieldName} = {polyfill.CSharpFieldName} | {polyfill.CSharpFieldBitMask}uL;");

        string GenerateIncludePreCondition(PolyfillData data)
        {
            var result = "";

            foreach (var requiredType in data.RequiredTypes)
            {
                result += requiredTypes.Single(t => t.TypeName == requiredType).CsharpFieldName + " && ";
            }

            if (data.ConditionalMembers.Length > 0)
            {
                result += "(";
                result += string.Join(" || ", data.ConditionalMembers.Select(member =>
                {
                    var dependency = polyfills.Single(p => p.TypeName == member);
                    return $"({dependency.CSharpFieldName} & {dependency.CSharpFieldBitMask}uL) == {dependency.CSharpFieldBitMask}uL";
                }));
                result += ") && ";
            }
            return result;
        }

        string GenerateIncludePostCondition(PolyfillData data)
        {
            var result = "";
            if (polyfill.TypeName.StartsWith("M:", StringComparison.Ordinal) && data.DeclaredMemberDocumentationIds.Length > 0)
            {
                result += " && (";
                result += string.Join(" && ", data.DeclaredMemberDocumentationIds.Select(member =>
                {
                    // Do not use "options" as the member cannot be part of Included or Excluded members
                    return $"IncludeMember(compilation, options: null, \"{member}\")";
                }));
                result += ")";
            }

            return result;
        }
    }
    sb.AppendLine("}");



    sb.AppendLine("public override int GetHashCode()");
    sb.AppendLine("{");
    sb.AppendLine("    var hash = _bits0.GetHashCode();");
    for (var i = 1; i < fieldCount; i++)
    {
        sb.AppendLine($"    hash = hash * 23 + _bits{i}.GetHashCode();");
    }

    sb.AppendLine("    return hash;");
    sb.AppendLine("}");



    sb.AppendLine("public override bool Equals(object? obj) => obj is Members other && Equals(other);");
    sb.Append("public bool Equals(Members other) => _bits0 == other._bits0");
    for (var i = 1; i < fieldCount; i++)
    {
        sb.Append($"  && _bits{i} == other._bits{i}");
    }
    sb.AppendLine(";");


    sb.AppendLine("public void AddSources(SourceProductionContext context)");
    sb.AppendLine("{");
    foreach (var polyfill in polyfills)
    {
        sb.AppendLine($"    if (({polyfill.CSharpFieldName} & {polyfill.CSharpFieldBitMask}ul) == {polyfill.CSharpFieldBitMask}ul)");
        sb.AppendLine($"        context.AddSource(\"{polyfill.OutputPath}\", PolyfillContents.{polyfill.CSharpSourceTextPropertyName});");
    }
    sb.AppendLine("}");

    sb.AppendLine("public string DumpAsCSharpComment()");
    sb.AppendLine("{");
    sb.AppendLine("    var sb = new StringBuilder();");
    sb.AppendLine("    sb.AppendLine(_options.DumpAsCSharpComment());");
    foreach (var requiredType in requiredTypes)
    {
        sb.AppendLine($"    sb.AppendLine(\"// {requiredType.TypeName}: \" + {requiredType.CsharpFieldName});");
    }

    sb.AppendLine("    sb.AppendLine(\"//\");");
    foreach (var polyfill in polyfills)
    {
        sb.AppendLine($"    sb.AppendLine(\"// {polyfill.TypeName}: \" + (({polyfill.CSharpFieldName} & {polyfill.CSharpFieldBitMask}ul) == {polyfill.CSharpFieldBitMask}ul));");
    }

    sb.AppendLine("    return sb.ToString();");
    sb.AppendLine("}");

    sb.AppendLine("}");

    sb.AppendLine("file static class PolyfillContents");
    sb.AppendLine("{");
    foreach (var polyfill in polyfills)
    {
        sb.AppendLine($"public static SourceText {polyfill.CSharpSourceTextPropertyName} {{ get; }} = SourceText.From(\"\"\"\"\"\"\"\"\"\"");
        sb.AppendLine(polyfill.PolyfillData.Content);
        sb.AppendLine("\"\"\"\"\"\"\"\"\"\", Encoding.UTF8);");
    }
    sb.AppendLine("}");

    Console.WriteLine(sb.ToString());
    Console.WriteLine("Polyfills: " + polyfills.Length);
    var path = GetMemberFilePath();
    Console.WriteLine(path);
    await File.WriteAllTextAsync(path, sb.ToString());
}

async Task GenerateReadme()
{
    var path = GetReadmeFilePath();

    var sb = new StringBuilder();

    sb.Append("### Types\n\n");
    var typeDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
          .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
          .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
          .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
          .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
          .WithMemberOptions(SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
          .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
          .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
          ;
    foreach (var polyfill in polyfills.Where(p => p.Kind is PolyfillKind.Type))
    {
        sb.Append($"- `{polyfill.Symbol.ToDisplayString(typeDisplayFormat)}`\n");
    }

    sb.Append("\n### Methods\n\n");

    var methodDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
          .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
          .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
          .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeTypeKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
          .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
          .WithMemberOptions(SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
          .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
          .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
          ;
    foreach (var polyfill in polyfills.Where(p => p.Kind is PolyfillKind.Method))
    {
        sb.Append($"- `{polyfill.Symbol.ToDisplayString(methodDisplayFormat)}`\n");
    }

    var content = await File.ReadAllTextAsync(path);
    var newContent = Regex.Replace(content, "(?<=<!-- begin_polyfills -->\\r?\\n).*(?=<!-- end_polyfills -->)", "\n" + sb.ToString() + "\n", RegexOptions.Singleline);
    await File.WriteAllTextAsync(path, newContent);
}

static string GetMemberFilePath() => GetRootPath() / "Meziantou.Polyfill" / "Members.g.cs";

static string GetReadmeFilePath() => GetRootPath() / "README.md";

static FullPath GetRootPath()
{
    var suffix = "Meziantou.Polyfill.slnx";
    var currentFolder = FullPath.CurrentDirectory();
    var fullPath = currentFolder / suffix;
    while (!File.Exists(fullPath))
    {
        currentFolder = currentFolder.Parent;
        if (currentFolder.IsEmpty)
            throw new Exception("Cannot find the path from " + FullPath.CurrentDirectory());

        fullPath = FullPath.Combine(currentFolder, suffix);
    }

    return fullPath.Parent;
}

string ReadResourceAsString(string name)
{
    using var sr = new StreamReader(assembly.GetManifestResourceStream(name)!);
    return sr.ReadToEnd();
}

static Polyfill[] SortPolyfills(Polyfill[] items)
{
    var result = new List<Polyfill>(items.Length);
    var remainingItems = items.ToList();
    while (remainingItems.Count > 0)
    {
        foreach (var item in remainingItems.Where(CanAddItem).OrderBy(i => i.Index).ToList())
        {
            result.Add(item);
            remainingItems.Remove(item);
        }
    }

    return [.. result];

    bool CanAddItem(Polyfill polyfill)
    {
        if (polyfill.PolyfillData.ConditionalMembers.Length == 0)
            return true;

        if (polyfill.PolyfillData.ConditionalMembers.All(m => result.Find(i => i.TypeName == m) != null))
            return true;

        return false;
    }
}

sealed class Polyfill
{
    public required int Index { get; set; }
    public required ISymbol Symbol { get; set; }
    public required string TypeName { get; set; }
    public required string CSharpName { get; set; }
    public required PolyfillData PolyfillData { get; set; }
    public required string OutputPath { get; set; }
    public required PolyfillKind Kind { get; set; }

    public string CSharpFieldName => "_bits" + (Index / 64);
    public int CSharpFieldBitIndex => Index % 64;
    public ulong CSharpFieldBitMask => 1uL << (Index % 64);
    public string CSharpSourceTextPropertyName => "Source_" + CSharpName;

    public override string ToString()
    {
        var symbolDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
            .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
            .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
            .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeTypeKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
            .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
            .WithMemberOptions(SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
            .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
            .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
            ;
        return Symbol.ToDisplayString(symbolDisplayFormat);
    }
}

enum PolyfillKind
{
    Type,
    Method,
}
