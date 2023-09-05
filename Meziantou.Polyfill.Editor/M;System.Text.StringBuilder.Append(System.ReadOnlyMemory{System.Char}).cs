using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlyMemory<char> value)
    {
        if (!value.IsEmpty)
        {
            unsafe
            {
                fixed (char* valueChars = &MemoryMarshal.GetReference(value.Span))
                {
                    _ = target.Append(valueChars, value.Length);
                }
            }
        }
        return target.Append(value.ToArray());
    }
}