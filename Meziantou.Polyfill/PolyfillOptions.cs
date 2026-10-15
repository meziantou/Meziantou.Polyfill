#pragma warning disable IDE0290 // Use primary constructor
using System.Globalization;
using System.Runtime.InteropServices;

namespace Meziantou.Polyfill;

internal sealed class PolyfillOptions : IEquatable<PolyfillOptions>
{
    private readonly string[]? _included;
    private readonly string[]? _excluded;

    public PolyfillOptions(string? included, string? excluded, bool generateDebugFile = false, string? minimumTargetFramework = null)
    {
        _included = ParseValues(included);
        _excluded = ParseValues(excluded);
        GenerateDebugFile = generateDebugFile;
        MinimumTargetFramework = string.IsNullOrEmpty(minimumTargetFramework) ? null : minimumTargetFramework;
        MinimumDotNetMajorVersion = ParseMinimumDotNetMajorVersion(minimumTargetFramework);
    }

    public bool GenerateDebugFile { get; }

    /// <summary>
    /// The raw value of the MeziantouPolyfill_MinimumTargetFramework property, kept for the debug file.
    /// </summary>
    public string? MinimumTargetFramework { get; }

    /// <summary>
    /// Oldest .NET (Core) major version the consumer compiles against, or 0 when unknown. Polyfills for
    /// APIs that already ship in that version (and in every newer one) can be skipped without inspecting
    /// the compilation. A single non-.NET moniker (netstandard*, net4*) in the list resets this to 0,
    /// because those frameworks are not comparable with .NET releases.
    /// </summary>
    public int MinimumDotNetMajorVersion { get; }

    public bool Include(string memberDocumentationId) => GetSkipReason(memberDocumentationId) is null;

    public string? GetSkipReason(string memberDocumentationId)
    {
        if (_excluded is not null)
        {
            foreach (var filter in _excluded)
            {
                if (memberDocumentationId.StartsWith(filter, StringComparison.Ordinal))
                    return "excluded by options";
            }
        }

        if (_included is not null)
        {
            foreach (var filter in _included)
            {
                if (memberDocumentationId.StartsWith(filter, StringComparison.Ordinal))
                    return null;
            }

            return "not included by options";
        }

        return null;
    }

    private static string[]? ParseValues(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        List<string>? values = null;
        foreach (var part in new SemiColonSplitEnumerator(value.AsSpan()))
        {
            if (part.IsEmpty)
                continue;

            if (part.Equals("*".AsSpan(), StringComparison.Ordinal))
                return null;

            values ??= [];
            values.Add(part.ToString());
        }

        if (values == null)
            return [];

        values.Sort(StringComparer.Ordinal);
        return [.. values];
    }

    private static int ParseMinimumDotNetMajorVersion(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return 0;

        var result = int.MaxValue;
        foreach (var part in new SemiColonSplitEnumerator(value.AsSpan()))
        {
            if (part.IsEmpty)
                continue;

            var major = ParseSingleTargetFramework(part);
            if (major == 0)
                return 0;

            if (major < result)
            {
                result = major;
            }
        }

        return result == int.MaxValue ? 0 : result;
    }

    // Recognizes "netX.Y" monikers (X >= 5), optionally followed by a platform suffix such as
    // "net8.0-windows10.0.19041.0". Anything else (netstandard2.0, net472, netcoreapp3.1, a future
    // moniker shape) returns 0, which disables the optimization.
    private static int ParseSingleTargetFramework(ReadOnlySpan<char> tfm)
    {
        var separator = tfm.IndexOf('-');
        if (separator >= 0)
        {
            tfm = tfm.Slice(0, separator);
        }

        if (tfm.Length < 5 || !tfm.StartsWith("net".AsSpan(), StringComparison.OrdinalIgnoreCase))
            return 0;

        tfm = tfm.Slice(3);
        var dot = tfm.IndexOf('.');
        if (dot <= 0)
            return 0;

        var major = 0;
        foreach (var c in tfm.Slice(0, dot))
        {
            if (c is < '0' or > '9')
                return 0;

            major = (major * 10) + (c - '0');
            if (major > 9999)
                return 0;
        }

        // net1.0 .. net4.x do not exist as .NET (Core) monikers; "net4.x" would be a typo for net4xx.
        return major >= 5 ? major : 0;
    }

    public override int GetHashCode() => GenerateDebugFile ? 1 : 0;
    public override bool Equals(object obj) => Equals(obj as PolyfillOptions);
    public bool Equals(PolyfillOptions? other)
    {
        if (other == null)
            return false;

        return GenerateDebugFile == other.GenerateDebugFile
            && MinimumDotNetMajorVersion == other.MinimumDotNetMajorVersion
            && string.Equals(MinimumTargetFramework, other.MinimumTargetFramework, StringComparison.Ordinal)
            && SequenceEqual(_included, other._included)
            && SequenceEqual(_excluded, other._excluded);

        static bool SequenceEqual(string[]? value1, string[]? value2)
        {
            if (value1 == value2)
                return true;

            if (value1 == null || value2 == null)
                return false;

            return value1.SequenceEqual(value2, StringComparer.Ordinal);
        }
    }

    public string DumpAsCSharpComment()
    {
        return "// IncludedMembers: " + (_included == null ? "<null>" : string.Join(";", _included)) + "\n"
             + "// ExcludedMembers: " + (_excluded == null ? "<null>" : string.Join(";", _excluded)) + "\n"
             + "// GenerateDebugFile: " + GenerateDebugFile + "\n"
             + "// MinimumTargetFramework: " + (MinimumTargetFramework ?? "<null>") + "\n"
             + "// MinimumDotNetMajorVersion: " + (MinimumDotNetMajorVersion == 0 ? "<none>" : MinimumDotNetMajorVersion.ToString(CultureInfo.InvariantCulture));
    }

    [StructLayout(LayoutKind.Auto)]
    private ref struct SemiColonSplitEnumerator
    {
        private ReadOnlySpan<char> _str;

        public SemiColonSplitEnumerator(ReadOnlySpan<char> str)
        {
            _str = str;
            Current = default;
        }

        public readonly SemiColonSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str;
            if (span.IsEmpty)
                return false;

            var index = span.IndexOfAny(';', '|');
            if (index == -1)
            {
                _str = []; // The remaining string is an empty string
                Current = span.Trim();
                return true;
            }

            Current = span.Slice(0, index).Trim();
            _str = span.Slice(index + 1);
            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }
    }
}
