using System.Runtime.InteropServices;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal readonly struct PolyfillEntry
{
    public readonly byte FieldIndex;
    public readonly byte ConditionalCount;
    public readonly byte DeclaredCount;

    // Oldest .NET (Core) major version that ships this API natively (255 when there is none). When the
    // consumer declares a minimum target framework at or above it, the polyfill can be skipped before
    // doing any symbol lookup.
    public readonly byte AvailableSinceDotNetMajorVersion;
    public readonly sbyte ProvidesFeatureBit; // -1 when the polyfill does not provide a required-type feature
    public readonly ushort ConditionalStart;
    public readonly ushort DeclaredStart;
    public readonly ulong Mask;
    public readonly ulong RequiredFeatures;
    public readonly string DocId;

    // One character per parameter ('-' by value, 'r' ref, 'o' out, 'i' in / ref readonly), or null when
    // the member has no by-ref parameter. Documentation IDs encode 'ref', 'in' and 'out' the same way, so
    // an existing member only counts as available when its parameter ref kinds match.
    public readonly string? ParameterRefKinds;

    public PolyfillEntry(byte fieldIndex, byte conditionalCount, byte declaredCount, byte availableSinceDotNetMajorVersion, sbyte providesFeatureBit, ushort conditionalStart, ushort declaredStart, ulong mask, ulong requiredFeatures, string docId, string? parameterRefKinds)
    {
        FieldIndex = fieldIndex;
        ConditionalCount = conditionalCount;
        DeclaredCount = declaredCount;
        AvailableSinceDotNetMajorVersion = availableSinceDotNetMajorVersion;
        ProvidesFeatureBit = providesFeatureBit;
        ConditionalStart = conditionalStart;
        DeclaredStart = declaredStart;
        Mask = mask;
        RequiredFeatures = requiredFeatures;
        DocId = docId;
        ParameterRefKinds = parameterRefKinds;
    }
}
