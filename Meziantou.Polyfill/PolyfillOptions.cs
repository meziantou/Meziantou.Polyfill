using System;
using System.Diagnostics.CodeAnalysis;

namespace Meziantou.Polyfill;

internal sealed class PolyfillOptions : IEquatable<PolyfillOptions>
{
    private string? _includedPolyfills;

    [AllowNull]
    public string IncludedPolyfills
    {
        get => _includedPolyfills ?? "";
        set => _includedPolyfills = value;
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(IncludedPolyfills);
    }

    public override bool Equals(object obj) => Equals(obj as PolyfillOptions);

    public bool Equals(PolyfillOptions? other)
    {
        if (other == null)
            return false;

        return IncludedPolyfills == other.IncludedPolyfills;
    }
}
