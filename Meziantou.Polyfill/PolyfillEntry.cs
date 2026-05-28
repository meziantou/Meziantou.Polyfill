using System.Runtime.InteropServices;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal readonly struct PolyfillEntry
{
    public readonly byte FieldIndex;
    public readonly byte ConditionalCount;
    public readonly byte DeclaredCount;
    public readonly sbyte ProvidesFeatureBit; // -1 when the polyfill does not provide a required-type feature
    public readonly ushort ConditionalStart;
    public readonly ushort DeclaredStart;
    public readonly ulong Mask;
    public readonly ulong RequiredFeatures;
    public readonly string DocId;

    public PolyfillEntry(byte fieldIndex, byte conditionalCount, byte declaredCount, sbyte providesFeatureBit, ushort conditionalStart, ushort declaredStart, ulong mask, ulong requiredFeatures, string docId)
    {
        FieldIndex = fieldIndex;
        ConditionalCount = conditionalCount;
        DeclaredCount = declaredCount;
        ProvidesFeatureBit = providesFeatureBit;
        ConditionalStart = conditionalStart;
        DeclaredStart = declaredStart;
        Mask = mask;
        RequiredFeatures = requiredFeatures;
        DocId = docId;
    }
}
