using System.Runtime.InteropServices;

namespace Meziantou.Polyfill;

[StructLayout(LayoutKind.Auto)]
internal readonly struct ConditionalCheck
{
    public readonly byte FieldIndex;
    public readonly ulong Mask;

    public ConditionalCheck(byte fieldIndex, ulong mask)
    {
        FieldIndex = fieldIndex;
        Mask = mask;
    }
}
