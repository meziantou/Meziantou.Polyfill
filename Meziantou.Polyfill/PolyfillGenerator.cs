using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Meziantou.Polyfill;

[Generator]
public sealed partial class PolyfillGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var options = context.AnalyzerConfigOptionsProvider.Select((options, cancellationToken) =>
        {
            return new PolyfillOptions()
            {
                IncludedPolyfills = GetValueOrDefault(options.GlobalOptions, "build_property.MeziantouPolyfill_IncludedPolyfills"),
                ExcludedPolyfills = GetValueOrDefault(options.GlobalOptions, "build_property.MeziantouPolyfill_ExcludedPolyfills"),
            };
        });

        var provider = context.CompilationProvider.Combine(options).Select((provider, cancellationToken) => new Members(provider.Left, provider.Right));

        context.RegisterImplementationSourceOutput(provider, (context, members) =>
        {
            context.AddSource("Debug.g.cs", SourceText.From(members.DumpAsCSharpComment(), Encoding.UTF8));

            members.AddSources(context);
        });
    }

    private static string? GetValueOrDefault(AnalyzerConfigOptions options, string key)
    {
        if (options.TryGetValue(key, out var value))
            return value;

        return null;
    }
}
