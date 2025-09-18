using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Meziantou.Polyfill.SourceGenerator.Tests;

public class UnitTest1
{
    private const string LatestDotnetPackageVersion = "10.0.0-rc.1.25451.107";
    private const string LatestDotnetTfm = "net10.0";

    [Fact]
    public void PolyfillOptions_Included()
    {
        var options = new PolyfillOptions("T:A;T:B", "");
        Assert.True(options.Include("T:A"));
        Assert.True(options.Include("T:B"));

        Assert.False(options.Include("T:C"));
    }

    [Fact]
    public void PolyfillOptions_Excluded()
    {
        var options = new PolyfillOptions("", "T:A|T:B");
        Assert.False(options.Include("T:A"));
        Assert.False(options.Include("T:B"));

        Assert.True(options.Include("T:C"));
    }

    [Fact]
    public async Task NoCodeGeneratedForLatestFramework()
    {
        var assemblies = await NuGetHelpers.GetNuGetReferences("Microsoft.NETCore.App.Ref", LatestDotnetPackageVersion, $"ref/{LatestDotnetTfm}/");
        var result = GenerateFiles("", assemblyLocations: assemblies);
        var tree = Assert.Single(result.GeneratorResult.GeneratedTrees);
        Assert.Equal("Meziantou.Polyfill/Meziantou.Polyfill.PolyfillGenerator/Debug.g.cs", tree.FilePath.Replace(Path.DirectorySeparatorChar, '/'));
    }

    [Fact]
    public async Task ExcludedPolyfill()
    {
        var assemblies = await NuGetHelpers.GetNuGetReferences("Microsoft.NETCore.App.Ref", "3.1.0", "ref/netcoreapp3.1/");

        var result = GenerateFiles("", assemblyLocations: assemblies);
        Assert.Single(result.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.Contains("UnscopedRefAttribute", StringComparison.Ordinal)));

        result = GenerateFiles("", assemblyLocations: assemblies, excludedPolyfills: "T:System.Diagnostics.CodeAnalysis.UnscopedRefAttribute");
        Assert.Empty(result.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.Contains("UnscopedRefAttribute", StringComparison.Ordinal)));
    }

    [Fact]
    public async Task IncludedPolyfill_Methods()
    {
        var assemblies = await NuGetHelpers.GetNuGetReferences("Microsoft.NETCore.App.Ref", "3.1.0", "ref/netcoreapp3.1/");

        var result = GenerateFiles("", assemblyLocations: assemblies);
        Assert.NotEmpty(result.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.Contains("WaitForExitAsync", StringComparison.Ordinal)));

        result = GenerateFiles("", assemblyLocations: assemblies, includedPolyfills: "M:System.Linq.Enumerable.OrderDescending``1(System.Collections.Generic.IEnumerable{``0},System.Collections.Generic.IComparer{``0})");
        Assert.Empty(result.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.Contains("WaitForExitAsync", StringComparison.Ordinal)));
        Assert.NotEmpty(result.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.Contains("System.Linq.Enumerable.OrderDescending", StringComparison.Ordinal)));
    }

    [Fact]
    public async Task InternalsVisibleTo_DoNotRegenerateExtensionMethods()
    {
        var assemblies = await NuGetHelpers.GetNuGetReferences("NETStandard.Library", "2.0.3", "build/");
        var tempGeneration = GenerateFiles("""[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("main")]""", assemblyName: "temp", assemblyLocations: assemblies);
        Assert.Single(tempGeneration.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.EndsWith("T_System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.g.cs", StringComparison.Ordinal)));
        Assert.Single(tempGeneration.GeneratorResult.GeneratedTrees.Where(t => t.FilePath.EndsWith("M_System.IO.TextReader.ReadToEndAsync(System.Threading.CancellationToken).g.cs", StringComparison.Ordinal)));

        var temp = Path.GetTempFileName() + ".dll";
        await File.WriteAllBytesAsync(temp, tempGeneration.Assembly!);
        var result = GenerateFiles("", assemblyName: "main", assemblyLocations: assemblies.Append(temp));
        Assert.Single(result.GeneratorResult.GeneratedTrees); // debug.g.cs
    }

    [Theory]
    [MemberData(nameof(GetConfigurations))]
    public async Task GeneratedCodeCompile(PackageReference[] packages)
    {
        var assemblies = new List<string>();
        foreach (var package in packages)
        {
            assemblies.AddRange(await NuGetHelpers.GetNuGetReferences(package.Name, package.Version, package.Path, package.Exclusions));
        }

        GenerateFiles("", assemblyLocations: [.. assemblies]);
    }

    [Fact]
    public async Task IsIncremental()
    {
        var assemblies = (await NuGetHelpers.GetNuGetReferences("Microsoft.NETCore.App.Ref", "3.1.0", "ref/netcoreapp3.1/"))
            .Select(loc => MetadataReference.CreateFromFile(loc))
            .ToArray();

        var compilation = CSharpCompilation.Create("TestProject",
           new[] { CSharpSyntaxTree.ParseText("struct Test { }") },
           assemblies,
           new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new PolyfillGenerator();
        var sourceGenerator = generator.AsSourceGenerator();

        // trackIncrementalGeneratorSteps allows to report info about each step of the generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: new ISourceGenerator[] { sourceGenerator },
            optionsProvider: new TestAnalyzerConfigOptionsProvider(new Dictionary<string, string?>(StringComparer.Ordinal)
            {
                ["build_property.MeziantouPolyfill_IncludedPolyfills"] = "*",
                ["build_property.MeziantouPolyfill_ExcludedPolyfills"] = "test",
            }),
            driverOptions: new GeneratorDriverOptions(default, trackIncrementalGeneratorSteps: true));

        // Run the generator
        driver = driver.RunGenerators(compilation);

        // Update the compilation and rerun the generator
        compilation = compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("// dummy"));
        driver = driver.RunGenerators(compilation);

        // Assert the driver doesn't recompute the output
        var result = driver.GetRunResult().Results.Single();
        var allOutputs = result.TrackedOutputSteps.SelectMany(outputStep => outputStep.Value).SelectMany(output => output.Outputs);
        var output = Assert.Single(allOutputs);
        Assert.Equal(IncrementalStepRunReason.Cached, output.Reason);

        // Assert the driver use the cached result
        var assemblyNameOutputs = result.TrackedSteps["Members"].Single().Outputs;
        output = Assert.Single(assemblyNameOutputs);
        Assert.Equal(IncrementalStepRunReason.Unchanged, output.Reason);
    }

    public static TheoryData<PackageReference[]> GetConfigurations()
    {
        return new TheoryData<PackageReference[]>
        {
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", LatestDotnetPackageVersion, $"ref/{LatestDotnetTfm}/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "9.0.0", "ref/net9.0/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "8.0.0", "ref/net8.0/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "7.0.5", "ref/net7.0/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "6.0.16", "ref/net6.0/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "5.0.0", "ref/net5.0/") } },
            { new[] { new PackageReference("Microsoft.NETCore.App.Ref", "3.1.0", "ref/netcoreapp3.1/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net481", "1.0.3", ""), new PackageReference("system.collections.immutable", "1.5.0", "lib/netstandard2.0/"), new PackageReference("System.Memory", "4.5.5", "lib/net461/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net481", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net48", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net472", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net471", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net47", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net462", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net461", "1.0.3", "") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net461", "1.0.3", ""), new PackageReference("System.Memory", "4.5.5", "lib/net461/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net461", "1.0.3", ""), new PackageReference("System.ValueTuple", "4.5.0", "lib/net461/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net461", "1.0.3", ""), new PackageReference("System.Memory", "4.5.5", "lib/net461/"), new PackageReference("System.ValueTuple", "4.5.0", "lib/net461/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net461", "1.0.3", ""), new PackageReference("System.Memory", "4.5.5", "lib/net461/"), new PackageReference("System.ValueTuple", "4.5.0", "lib/net461/"), new PackageReference("System.Net.Http", "4.3.4", "lib/net46/") } },
            { new[] { new PackageReference("Microsoft.NETFramework.ReferenceAssemblies.net46", "1.0.3", "") } },
            { new[] { new PackageReference("NETStandard.Library", "2.0.3", "") } },
            { new[] { new PackageReference("NETStandard.Library", "2.0.3", ""), new PackageReference("System.ValueTuple", "4.5.0", "lib/netstandard2.0/") } },
            { new[] { new PackageReference("NETStandard.Library", "2.0.3", ""), new PackageReference("System.Memory", "4.5.5", "lib/netstandard2.0/") } },
            { new[] { new PackageReference("NETStandard.Library", "2.0.3", ""), new PackageReference("System.ValueTuple", "4.5.0", "lib/netstandard2.0/"), new PackageReference("System.Memory", "4.5.5", "lib/netstandard2.0/") } },
        };
    }

    [SuppressMessage("Design", "CA1034:Nested types should not be visible")]
    public sealed class PackageReference : IXunitSerializable
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }

        [SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public string[]? Exclusions { get; set; }

        public PackageReference()
            : this("", "", "")
        {
        }

        public PackageReference(string name, string version, string path)
        {
            Name = name;
            Version = version;
            Path = path;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Name = info.GetValue<string>("Name");
            Version = info.GetValue<string>("Version");
            Path = info.GetValue<string>("Path");
            Exclusions = info.GetValue<string[]>("Exclusions");
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("Name", Name);
            info.AddValue("Version", Version);
            info.AddValue("Path", Path);
            info.AddValue("Exclusions", Exclusions);
        }
    }

    private static (GeneratorDriverRunResult GeneratorResult, Compilation OutputCompilation, byte[]? Assembly) GenerateFiles(string file, string assemblyName = "compilation", bool mustCompile = true, IEnumerable<string>? assemblyLocations = null, string? includedPolyfills = null, string? excludedPolyfills = null)
    {
        assemblyLocations ??= Array.Empty<string>();
        var references = assemblyLocations
            .Select(loc => MetadataReference.CreateFromFile(loc))
            .ToArray();

        var options = new CSharpParseOptions(languageVersion: LanguageVersion.Preview);

        var compilation = CSharpCompilation.Create(assemblyName,
            new[] { CSharpSyntaxTree.ParseText(file, options) },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        var generator = new PolyfillGenerator().AsSourceGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: new ISourceGenerator[] { generator },
            parseOptions: options,
            optionsProvider: new TestAnalyzerConfigOptionsProvider(new Dictionary<string, string?>(StringComparer.Ordinal)
            {
                ["build_property.MeziantouPolyfill_IncludedPolyfills"] = includedPolyfills,
                ["build_property.MeziantouPolyfill_ExcludedPolyfills"] = excludedPolyfills,
            }));

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
        Assert.Empty(diagnostics);

        var runResult = driver.GetRunResult();

        // Validate the output project compiles
        using var ms = new MemoryStream();
        var result = outputCompilation.Emit(ms);
        if (mustCompile)
        {
            var tree = runResult.GeneratedTrees.FirstOrDefault(tree => tree.FilePath == "Meziantou.Polyfill\\Meziantou.Polyfill.PolyfillGenerator\\Debug.g.cs");
            var diags = string.Join("\n", result.Diagnostics);
            Assert.True(result.Success, "Compilation error:\n" + diags + "\n" + tree);
            Assert.Empty(result.Diagnostics);
        }

        return (runResult, outputCompilation, result.Success ? ms.ToArray() : null);
    }

    private sealed class TestAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
    {
        private readonly Dictionary<string, string?> _values;

        public TestAnalyzerConfigOptionsProvider(Dictionary<string, string?> values)
        {
            _values = values ?? [];
        }

        public override AnalyzerConfigOptions GlobalOptions => new TestAnalyzerConfigOptions(_values);
        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => new TestAnalyzerConfigOptions(_values);
        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => new TestAnalyzerConfigOptions(_values);

        private sealed class TestAnalyzerConfigOptions : AnalyzerConfigOptions
        {
            private readonly Dictionary<string, string?> _values;

            public TestAnalyzerConfigOptions(Dictionary<string, string?> values)
            {
                _values = values;
            }

            public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
            {
                return _values.TryGetValue(key, out value);
            }
        }
    }

    private static class NuGetHelpers
    {
        private static readonly ConcurrentDictionary<string, Lazy<Task<string[]>>> Cache = new(StringComparer.Ordinal);

        public static Task<string[]> GetNuGetReferences(string packageName, string version, string path, string[]? exclusions = null)
        {
            string key = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(packageName + '@' + version + ':' + path + (exclusions is null ? "" : string.Join(":", exclusions)))));
            var task = Cache.GetOrAdd(key, key =>
            {
                return new Lazy<Task<string[]>>(Download);
            });

            return task.Value;

            async Task<string[]> Download()
            {
                var tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Meziantou.PolyfillTests", "ref", packageName + '@' + version);
                if (!Directory.Exists(tempFolder) || !Directory.EnumerateFileSystemEntries(tempFolder).Any())
                {
                    Directory.CreateDirectory(tempFolder);
                    using var httpClient = new HttpClient();
                    using var stream = await httpClient.GetStreamAsync(new Uri($"https://www.nuget.org/api/v2/package/{packageName}/{version}")).ConfigureAwait(false);
                    using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

                    foreach (var entry in zip.Entries)
                    {
                        var extractPath = Path.Combine(tempFolder, entry.FullName);
                        Directory.CreateDirectory(Path.GetDirectoryName(extractPath)!);
#if NET10_0_OR_GREATER
                        await entry.ExtractToFileAsync(extractPath, overwrite: true);
#else
                        entry.ExtractToFile(extractPath, overwrite: true);
#endif
                    }
                }

                var dlls = Directory.GetFiles(tempFolder, "*.dll", SearchOption.AllDirectories);

                // Filter invalid .NET assembly
                var result = new List<string>();
                foreach (var dll in dlls)
                {
                    if (Path.GetFileName(dll) == "System.EnterpriseServices.Wrapper.dll")
                        continue;

                    var relativePath = Path.GetRelativePath(tempFolder, dll).Replace('\\', '/');
                    if (!relativePath.StartsWith(path, StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (exclusions != null)
                    {
                        if (exclusions.Any(exclusion => relativePath.StartsWith(exclusion, StringComparison.OrdinalIgnoreCase)))
                            continue;
                    }

                    try
                    {
                        using var stream = File.OpenRead(dll);
                        using var peFile = new PEReader(stream);
                        var metadataReader = peFile.GetMetadataReader();
                        result.Add(dll);
                    }
                    catch
                    {
                    }
                }

                return [.. result];
            }
        }
    }
}