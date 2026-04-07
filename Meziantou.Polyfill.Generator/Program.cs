#pragma warning disable MA0011 // IFormatProvider is missing
#pragma warning disable MA0047 // Declare types in namespaces
#pragma warning disable MA0048 // File name must match type name
using System.IO.Compression;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
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

// Write a txt file containing all available types and methods as XML Documentation Id (for debugging purpose)
if (args.Contains("--generate-all-symbols"))
{
    var allDocIds = string.Join('\n', compilation.References
        .OfType<PortableExecutableReference>()
        .Select(compilation.GetAssemblyOrModuleSymbol)
        .OfType<IAssemblySymbol>()
        .SelectMany(GetAllTypes)
        .SelectMany(typeSymbol => new[] { typeSymbol }.Concat(typeSymbol.GetMembers().OfType<IMethodSymbol>().Cast<ISymbol>()).Concat(typeSymbol.GetMembers().OfType<IPropertySymbol>().Cast<ISymbol>()))
        .Select(DocumentationCommentId.CreateDeclarationId)
        .Distinct(StringComparer.Ordinal)
        .Order(StringComparer.Ordinal));
    await File.WriteAllTextAsync(GetRootPath() / "Meziantou.Polyfill.Editor" / "_AllSymbols.txt", allDocIds);
}

//
var assembly = Assembly.GetExecutingAssembly();
var polyfills = assembly.GetManifestResourceNames()
      .OrderBy(_ => _, StringComparer.Ordinal)
      .Select((item, index) =>
      {
          var xmlDocumentationId = Path.GetFileNameWithoutExtension(item).Replace(';', ':');
          var polyfillData = PolyfillData.Get(compilation, xmlDocumentationId, $$""""
                  #pragma warning disable
                  #nullable enable annotations
                  {{ReadResourceAsString(item)}}
                  """");
          xmlDocumentationId = polyfillData.XmlDocumentationId ?? xmlDocumentationId;

          var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(xmlDocumentationId, compilation);
          if (symbols.Length == 0)
          {
              throw new InvalidOperationException($"Cannot find symbol for '{xmlDocumentationId}' (resource name: {item}). Polyfill content:\n{polyfillData}");
          }

          if (symbols.Length > 1)
          {
              throw new InvalidOperationException($"Multiple symbols found for '{xmlDocumentationId}' (resource name: {item}): {string.Join(", ", symbols.Select(s => s.ToDisplayString()))}");
          }

          var symbol = symbols[0];
          return new Polyfill()
          {
              Index = index,
              TypeName = xmlDocumentationId,
              Symbol = symbol,
              Kind = symbol.Kind switch
              {
                  SymbolKind.NamedType => PolyfillKind.Type,
                  SymbolKind.Method => PolyfillKind.Method,
                  SymbolKind.Property => PolyfillKind.Property,
                  SymbolKind.Field => PolyfillKind.Property,
                  _ => throw new InvalidOperationException($"Unknown symbol kind '{symbol.Kind}'"),
              },
              OutputPath = Path.GetFileNameWithoutExtension(item)
                               .Replace(';', '_')
                               .Replace('~', '_')
                               .Replace('@', '_') + ".g.cs",
              CSharpName = Path.GetFileNameWithoutExtension(item)
                               .Replace(';', '_')
                               .Replace('~', '_')
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
              PolyfillData = polyfillData,
          };
      })
      .ToArray();

// Ensure there is no duplicated polyfill
var duplicatePolyfills = polyfills.GroupBy(p => p.TypeName, StringComparer.Ordinal).Where(g => g.Count() > 1).ToArray();
if (duplicatePolyfills.Length > 0)
{
    var sb = new StringBuilder();
    foreach (var group in duplicatePolyfills)
    {
        sb.AppendLine($"- {group.Key}");
    }
    throw new InvalidOperationException("There are duplicated polyfills:\n" + sb.ToString());
}

// Detect which TFMs support each polyfill, sort by earliest supported version, then reassign indices
await DetectAndAssignVersionsAsync(polyfills, compilation, GetRootPath());

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
    using Microsoft.CodeAnalysis.CSharp;
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
    sb.AppendLine($"private readonly bool _supportExtensions;");
    sb.AppendLine($"private readonly bool _supportUnsafe;");
    sb.AppendLine($"private readonly string _extraDefines;");

    foreach (var requiredType in requiredTypes)
    {
        sb.AppendLine($"private readonly bool {requiredType.CsharpFieldName};");
    }

    sb.AppendLine("public Members(Compilation compilation, PolyfillOptions options)");
    sb.AppendLine("{");
    sb.AppendLine("    _options = options;");
    sb.AppendLine("    _supportExtensions = Enum.IsDefined(typeof(LanguageVersion), 1400) && compilation.SyntaxTrees.FirstOrDefault()?.Options is CSharpParseOptions parseOptions && parseOptions.LanguageVersion >= (LanguageVersion)1400;");
    sb.AppendLine("    _supportUnsafe = compilation.Options is CSharpCompilationOptions compilationOptions && compilationOptions.AllowUnsafe;");

    foreach (var requiredType in requiredTypes)
    {
        sb.AppendLine($"    {requiredType.CsharpFieldName} = compilation.GetTypeByMetadataName(\"{requiredType.TypeName}\") != null;");
    }

    sb.AppendLine($"    _extraDefines = \"\";");
    sb.AppendLine("    if (_supportUnsafe) _extraDefines += \"#define MEZIANTOU_POLYFILL_SUPPORT_UNSAFE\\n\";");
    foreach (var requiredType in requiredTypes)
    {
        var defineName = requiredType.TypeName.Replace('`', '_').Replace('.', '_').ToUpperInvariant();
        sb.AppendLine($"    if({requiredType.CsharpFieldName}) {{ _extraDefines += \"#define MEZIANTOU_POLYFILL_SUPPORT_{defineName}\\n\"; }}");
    }

    sb.AppendLine($"    var includeContext = new IncludeContext(compilation, options);");
    foreach (var polyfill in polyfills)
    {
        var requiredType = requiredTypes.SingleOrDefault(rt => $"T:{rt.TypeName}" == polyfill.PolyfillData.XmlDocumentationId);
        if (requiredType is null)
            continue;

        sb.AppendLine($"    if ({GenerateIncludePreCondition(polyfill.PolyfillData)}IncludeMember(includeContext, \"{polyfill.TypeName}\"){GenerateIncludePostCondition(polyfill.PolyfillData)})");
        sb.AppendLine($"    {{");
        sb.AppendLine($"        {polyfill.CSharpFieldName} = {polyfill.CSharpFieldName} | {polyfill.CSharpFieldBitMask}uL;");
        sb.AppendLine($"        {requiredType.CsharpFieldName} = true;");
        sb.AppendLine($"    }}");
    }

    foreach (var polyfill in polyfills)
    {
        var requiredType = requiredTypes.SingleOrDefault(rt => $"T:{rt.TypeName}" == polyfill.PolyfillData.XmlDocumentationId);
        if (requiredType is not null)
            continue;

        sb.AppendLine($"    if ({GenerateIncludePreCondition(polyfill.PolyfillData)}IncludeMember(includeContext, \"{polyfill.TypeName}\"){GenerateIncludePostCondition(polyfill.PolyfillData)})");
        sb.AppendLine($"        {polyfill.CSharpFieldName} = {polyfill.CSharpFieldName} | {polyfill.CSharpFieldBitMask}uL;");

    }
    sb.AppendLine("}");


    string GenerateIncludePreCondition(PolyfillData data)
    {
        var result = "";

        if (data.UseExtensions)
        {
            result += "_supportExtensions && ";
        }

        if (data.UseUnsafe)
        {
            result += "_supportUnsafe && ";
        }

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
        if (data.XmlDocumentationId?.StartsWith("T:", StringComparison.Ordinal) == true && data.DeclaredMemberDocumentationIds.Length > 0)
        {
            result += " && (";
            result += string.Join(" && ", data.DeclaredMemberDocumentationIds.Select(member =>
            {
                // Do not use "options" as the member cannot be part of Included or Excluded members
                return $"IncludeMember(includeContext, \"{member}\")";
            }));
            result += ")";
        }

        return result;
    }

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
    foreach (var group in polyfills.GroupBy(p => p.CSharpFieldName, StringComparer.Ordinal))
    {
        sb.AppendLine($"    if ({group.Key} != 0)");
        sb.AppendLine("    {");
        foreach (var polyfill in group)
        {
            sb.AppendLine($"        if (({polyfill.CSharpFieldName} & {polyfill.CSharpFieldBitMask}ul) == {polyfill.CSharpFieldBitMask}ul)");
            sb.AppendLine($"            AddSource(context, \"{polyfill.OutputPath}\", PolyfillContents.{polyfill.CSharpSourceTextPropertyName});");
        }
        sb.AppendLine("    }");
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

    // Generate the method to create PolyfillExtensions declarations with [EmbeddedAttribute]
    sb.AppendLine("public static string GetPolyfillExtensionsDeclarations()");
    sb.AppendLine("{");
    sb.AppendLine("    var sb = new StringBuilder();");
    sb.AppendLine("    sb.AppendLine(\"// <auto-generated/>\");");
    sb.AppendLine("    sb.AppendLine(\"#pragma warning disable\");");

    // Collect all unique PolyfillExtensions class names
    var allPolyfillExtensionsClassNames = polyfills
        .SelectMany(p => p.PolyfillData.PolyfillExtensionsClassNames)
        .Distinct(StringComparer.Ordinal)
        .Order(StringComparer.Ordinal)
        .ToArray();

    foreach (var className in allPolyfillExtensionsClassNames)
    {
        sb.AppendLine($"    sb.AppendLine(\"[Microsoft.CodeAnalysis.EmbeddedAttribute]\");");
        sb.AppendLine($"    sb.AppendLine(\"[System.CodeDom.Compiler.GeneratedCodeAttribute(\\\"Meziantou.Polyfill\\\", \\\"\\\")]\");");
        sb.AppendLine($"    sb.AppendLine(\"internal static partial class {className}\");");
        sb.AppendLine($"    sb.AppendLine(\"{{\");");
        sb.AppendLine($"    sb.AppendLine(\"}}\");");
    }

    sb.AppendLine("    return sb.ToString();");
    sb.AppendLine("}");

    sb.AppendLine("}");

    sb.AppendLine("file static class PolyfillContents");
    sb.AppendLine("{");
    foreach (var polyfill in polyfills)
    {
        sb.AppendLine($"public static string {polyfill.CSharpSourceTextPropertyName} {{ get; }} = \"\"\"\"\"\"\"\"\"\"");
        sb.AppendLine(polyfill.PolyfillData.Content);
        sb.AppendLine("\"\"\"\"\"\"\"\"\"\";");
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

    sb.Append($"### Types ({polyfills.Where(p => p.Kind is PolyfillKind.Type).Count()})\n\n");
    var typeDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
          .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
          .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
          .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
          .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
          .WithMemberOptions(SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
          .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
          .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
          ;
    foreach (var polyfill in polyfills.Where(p => p.Kind is PolyfillKind.Type).OrderBy(p => p.TypeName, StringComparer.Ordinal))
    {
        sb.Append($"- `{polyfill.Symbol.ToDisplayString(typeDisplayFormat)}`\n");
    }

    sb.Append($"\n### Methods ({polyfills.Where(p => p.Kind is PolyfillKind.Method).Count()})\n\n");

    var methodDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
          .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
          .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
          .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeTypeKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
          .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
          .WithMemberOptions(SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
          .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
          .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
          ;
    foreach (var polyfill in polyfills.Where(p => p.Kind is PolyfillKind.Method).OrderBy(p => p.TypeName, StringComparer.Ordinal))
    {
        sb.Append($"- `{polyfill.Symbol.ToDisplayString(methodDisplayFormat)}`\n");
    }

    sb.Append($"\n### Properties ({polyfills.Where(p => p.Kind is PolyfillKind.Property).Count()})\n\n");

    var propertyDisplayFormat = SymbolDisplayFormat.FullyQualifiedFormat
          .WithGenericsOptions(SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints)
          .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining)
          .WithKindOptions(SymbolDisplayKindOptions.IncludeMemberKeyword | SymbolDisplayKindOptions.IncludeTypeKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword)
          .WithLocalOptions(SymbolDisplayLocalOptions.IncludeModifiers | SymbolDisplayLocalOptions.IncludeConstantValue | SymbolDisplayLocalOptions.IncludeType)
          .WithMemberOptions(SymbolDisplayMemberOptions.IncludeExplicitInterface | SymbolDisplayMemberOptions.IncludeContainingType | SymbolDisplayMemberOptions.IncludeParameters | SymbolDisplayMemberOptions.IncludeConstantValue | SymbolDisplayMemberOptions.IncludeRef)
          .WithParameterOptions(SymbolDisplayParameterOptions.IncludeExtensionThis | SymbolDisplayParameterOptions.IncludeModifiers | SymbolDisplayParameterOptions.IncludeType | SymbolDisplayParameterOptions.IncludeName | SymbolDisplayParameterOptions.IncludeDefaultValue | SymbolDisplayParameterOptions.IncludeOptionalBrackets)
          .WithMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.AllowDefaultLiteral | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers | SymbolDisplayMiscellaneousOptions.ExpandNullable | SymbolDisplayMiscellaneousOptions.ExpandValueTuple)
          ;
    foreach (var polyfill in polyfills.Where(p => p.Kind is PolyfillKind.Property).OrderBy(p => p.TypeName, StringComparer.Ordinal))
    {
        sb.Append($"- `{polyfill.Symbol.ToDisplayString(propertyDisplayFormat)}`\n");
    }

    var content = await File.ReadAllTextAsync(path);
    var newContent = Regex.Replace(content, "(?<=<!-- begin_polyfills -->\\r?\\n).*(?=<!-- end_polyfills -->)", "\n" + sb.ToString() + "\n", RegexOptions.Singleline, Timeout.InfiniteTimeSpan);
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
            throw new InvalidOperationException("Cannot find the path from " + FullPath.CurrentDirectory());

        fullPath = FullPath.Combine(currentFolder, suffix);
    }

    return fullPath.Parent;
}

string ReadResourceAsString(string name)
{
    using var sr = new StreamReader(assembly.GetManifestResourceStream(name)!);
    return sr.ReadToEnd();
}

static async Task DetectAndAssignVersionsAsync(Polyfill[] polyfills, CSharpCompilation currentCompilation, string rootPath)
{
    var nugetPackagesPath = GetNuGetPackagesPath();
    Console.WriteLine($"NuGet packages path: {nugetPackagesPath}");

    // Define required reference assembly packages and their TFM mappings
    var requiredPackages = new (string PackageId, string Version, string TfmName, string RelativeRefPath)[]
    {
        ("netstandard.library", "2.0.3", "netstandard2.0", Path.Combine("build", "netstandard2.0", "ref")),
        ("netstandard.library.ref", "2.1.0", "netstandard2.1", Path.Combine("ref", "netstandard2.1")),
        ("microsoft.netframework.referenceassemblies.net462", "1.0.3", "net462", Path.Combine("build", ".NETFramework", "v4.6.2")),
        ("microsoft.netframework.referenceassemblies.net472", "1.0.3", "net472", Path.Combine("build", ".NETFramework", "v4.7.2")),
        ("microsoft.netframework.referenceassemblies.net48", "1.0.3", "net48", Path.Combine("build", ".NETFramework", "v4.8")),
        ("microsoft.netcore.app.ref", "6.0.0", "net6.0", Path.Combine("ref", "net6.0")),
        ("microsoft.netcore.app.ref", "7.0.0", "net7.0", Path.Combine("ref", "net7.0")),
        ("microsoft.netcore.app.ref", "8.0.0", "net8.0", Path.Combine("ref", "net8.0")),
        ("microsoft.netcore.app.ref", "9.0.0", "net9.0", Path.Combine("ref", "net9.0")),
        ("microsoft.netcore.app.ref", "10.0.0", "net10.0", Path.Combine("ref", "net10.0")),
    };

    // Build TFM definitions from known package list
    var tfmDefinitions = requiredPackages.Select(p => (
        Name: p.TfmName,
        RefPath: Path.Combine(nugetPackagesPath, p.PackageId, p.Version, p.RelativeRefPath)
    )).ToList();

    // Include current runtime if not already covered
    var currentVersionName = $"net{Environment.Version.Major}.0";
    var includeCurrentRuntime = !tfmDefinitions.Any(t => string.Equals(t.Name, currentVersionName, StringComparison.Ordinal));

    var allVersionNames = tfmDefinitions.Select(t => t.Name).ToList();
    if (includeCurrentRuntime)
    {
        allVersionNames.Add(currentVersionName);
    }

    // Build version order for sorting
    var versionOrder = new Dictionary<string, int>(StringComparer.Ordinal);
    for (var i = 0; i < allVersionNames.Count; i++)
    {
        versionOrder[allVersionNames[i]] = i;
    }

    // Try to use cached version data to skip expensive assembly loading and package downloading
    var cacheFilePath = Path.Combine(rootPath, "Meziantou.Polyfill.Generator", "polyfill-supported-versions.json");
    if (!TryLoadFromCache(cacheFilePath, polyfills, allVersionNames))
    {
        // Cache miss - ensure required packages are downloaded
        await EnsurePackagesDownloadedAsync(nugetPackagesPath, requiredPackages);

        // Filter to TFMs with existing reference directories
        var availableTfms = new List<(string Name, string RefPath)>();
        foreach (var (name, refPath) in tfmDefinitions)
        {
            if (Directory.Exists(refPath))
            {
                availableTfms.Add((name, refPath));
            }
            else
            {
                Console.WriteLine($"Warning: Reference assemblies not found for {name} at {refPath}");
            }
        }

        // Load assemblies and check each polyfill
        var compilations = new List<(string Name, int SortOrder, CSharpCompilation Compilation)>();
        for (var i = 0; i < availableTfms.Count; i++)
        {
            var (name, refPath) = availableTfms[i];
            var files = Directory.GetFiles(refPath, "*.dll").ToList();
            var facadesPath = Path.Combine(refPath, "Facades");
            if (Directory.Exists(facadesPath))
            {
                files.AddRange(Directory.GetFiles(facadesPath, "*.dll"));
            }

            var comp = CSharpCompilation.Create(
                assemblyName: $"version-check-{name}",
                references: files.Select(f => MetadataReference.CreateFromFile(f)),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, metadataImportOptions: MetadataImportOptions.All));

            compilations.Add((name, i, comp));
            Console.WriteLine($"Loaded {files.Count} assemblies for {name}");
        }

        if (includeCurrentRuntime)
        {
            compilations.Add((currentVersionName, availableTfms.Count, currentCompilation));
            Console.WriteLine($"Using current runtime as {currentVersionName}");
        }

        Console.WriteLine($"Checking {polyfills.Length} polyfills against {compilations.Count} TFMs...");
        foreach (var polyfill in polyfills)
        {
            var supportedVersions = new List<string>();
            foreach (var (name, _, comp) in compilations)
            {
                var symbols = DocumentationCommentId.GetSymbolsForDeclarationId(polyfill.TypeName, comp);
                if (symbols.Length > 0)
                {
                    supportedVersions.Add(name);
                }
            }

            polyfill.SupportedInVersions = [.. supportedVersions];
        }

        // Write cache file
        var cacheEntries = new SortedDictionary<string, string[]>(StringComparer.Ordinal);
        foreach (var polyfill in polyfills)
        {
            cacheEntries[polyfill.TypeName] = polyfill.SupportedInVersions;
        }

        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(cacheFilePath, JsonSerializer.Serialize(new { versions = allVersionNames.ToArray(), polyfills = cacheEntries }, jsonOptions));
        Console.WriteLine($"Wrote version cache to {cacheFilePath}");
    }

    // Sort by earliest supported version (ascending), then by name, and reassign Index
    var sortedPolyfills = polyfills
        .OrderBy(p => GetEarliestVersionOrder(p.SupportedInVersions, versionOrder))
        .ThenBy(p => p.TypeName, StringComparer.Ordinal)
        .ToArray();

    for (var i = 0; i < sortedPolyfills.Length; i++)
    {
        sortedPolyfills[i].Index = i;
    }

    // Print version distribution summary
    foreach (var group in sortedPolyfills.GroupBy(p => GetEarliestVersionOrder(p.SupportedInVersions, versionOrder)).OrderBy(g => g.Key))
    {
        var versionName = group.Key == int.MaxValue ? "unknown" : allVersionNames[group.Key];
        Console.WriteLine($"  {versionName}: {group.Count()} polyfills (bits {group.Min(p => p.Index)}\u2013{group.Max(p => p.Index)})");
    }

    static int GetEarliestVersionOrder(string[] supportedVersions, Dictionary<string, int> versionOrder)
    {
        if (supportedVersions.Length == 0)
            return int.MaxValue;

        return supportedVersions.Min(v => versionOrder.GetValueOrDefault(v, int.MaxValue));
    }

    static string GetNuGetPackagesPath()
    {
        var path = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
        if (!string.IsNullOrEmpty(path))
            return path;

        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
    }

    static bool TryLoadFromCache(string cacheFilePath, Polyfill[] polyfills, List<string> expectedVersionNames)
    {
        if (!File.Exists(cacheFilePath))
            return false;

        try
        {
            var json = File.ReadAllText(cacheFilePath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // Check versions match exactly
            var cachedVersions = root.GetProperty("versions").EnumerateArray().Select(v => v.GetString()!).ToList();
            if (!cachedVersions.SequenceEqual(expectedVersionNames, StringComparer.Ordinal))
            {
                Console.WriteLine("Cache miss: version list changed");
                return false;
            }

            // Check all polyfills are present in cache and no extras
            var cachedPolyfills = root.GetProperty("polyfills");
            var polyfillTypeNames = new HashSet<string>(polyfills.Select(p => p.TypeName), StringComparer.Ordinal);
            var cachedPolyfillNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var prop in cachedPolyfills.EnumerateObject())
            {
                cachedPolyfillNames.Add(prop.Name);
            }

            if (!polyfillTypeNames.SetEquals(cachedPolyfillNames))
            {
                Console.WriteLine("Cache miss: polyfill list changed");
                return false;
            }

            // Populate SupportedInVersions from cache
            foreach (var polyfill in polyfills)
            {
                polyfill.SupportedInVersions = cachedPolyfills.GetProperty(polyfill.TypeName)
                    .EnumerateArray()
                    .Select(v => v.GetString()!)
                    .ToArray();
            }

            Console.WriteLine("Using cached version data (skipped assembly loading)");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
            return false;
        }
    }

    static async Task EnsurePackagesDownloadedAsync(string nugetPackagesPath, (string PackageId, string Version, string TfmName, string RelativeRefPath)[] packages)
    {
        var packagesToDownload = packages
            .Select(p => (p.PackageId, p.Version))
            .Distinct()
            .Where(p => !Directory.Exists(Path.Combine(nugetPackagesPath, p.PackageId, p.Version)))
            .ToArray();

        if (packagesToDownload.Length == 0)
        {
            Console.WriteLine("All reference assembly packages already in cache");
            return;
        }

        Console.WriteLine($"Downloading {packagesToDownload.Length} missing reference assembly package(s)...");

        using var httpClient = new HttpClient();
        foreach (var (packageId, version) in packagesToDownload)
        {
            var url = $"https://api.nuget.org/v3-flatcontainer/{packageId}/{version}/{packageId}.{version}.nupkg";
            Console.WriteLine($"  Downloading {packageId}@{version}...");

            using var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var packageStream = await response.Content.ReadAsStreamAsync();
            using var archive = new ZipArchive(packageStream, ZipArchiveMode.Read);

            var packageDir = Path.Combine(nugetPackagesPath, packageId, version);
            Directory.CreateDirectory(packageDir);
            archive.ExtractToDirectory(packageDir);

            Console.WriteLine($"  Installed {packageId}@{version}");
        }
    }
}

static Polyfill[] SortPolyfills(Polyfill[] items)
{
    var result = new List<Polyfill>(items.Length);
    var remainingItems = items.ToList();
    while (remainingItems.Count > 0)
    {
        var itemAdded = false;
        foreach (var item in remainingItems.Where(CanAddItem).OrderBy(i => i.Index).ToList())
        {
            result.Add(item);
            remainingItems.Remove(item);
            itemAdded = true;
        }

        if (!itemAdded)
        {
            throw new InvalidOperationException("Cannot sort polyfills due to circular dependencies: " + string.Join(", ", remainingItems.Select(i => i.TypeName)));
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

static IEnumerable<ITypeSymbol> GetAllTypes(IAssemblySymbol assembly)
{
    var result = new List<ITypeSymbol>();
    foreach (var module in assembly.Modules)
    {
        ProcessNamespace(result, module.GlobalNamespace);
    }

    return result;

    static void ProcessNamespace(List<ITypeSymbol> result, INamespaceSymbol ns)
    {
        foreach (var type in ns.GetTypeMembers())
        {
            ProcessType(result, type);
        }

        foreach (var nestedNs in ns.GetNamespaceMembers())
        {
            ProcessNamespace(result, nestedNs);
        }
    }

    static void ProcessType(List<ITypeSymbol> result, ITypeSymbol symbol)
    {
        result.Add(symbol);
        foreach (var type in symbol.GetTypeMembers())
        {
            ProcessType(result, type);
        }
    }
}

internal sealed class Polyfill
{
    public required int Index { get; set; }
    public required ISymbol Symbol { get; set; }
    public required string TypeName { get; set; }
    public required string CSharpName { get; set; }
    public required PolyfillData PolyfillData { get; set; }
    public required string OutputPath { get; set; }
    public required PolyfillKind Kind { get; set; }

    public string[] SupportedInVersions { get; set; } = [];

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

internal enum PolyfillKind
{
    Type,
    Method,
    Property,
}
