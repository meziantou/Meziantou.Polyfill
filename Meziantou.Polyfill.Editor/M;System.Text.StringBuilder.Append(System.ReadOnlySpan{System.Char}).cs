using System;
using System.Runtime.InteropServices;
using System.Text;

static partial class PolyfillExtensions
{
    public static StringBuilder Append(this StringBuilder target, ReadOnlySpan<char> value)
    {
        if (!value.IsEmpty)
        {
            unsafe
            {
                fixed (char* valueChars = &MemoryMarshal.GetReference(value))
                {
                    _ = target.Append(valueChars, value.Length);
                }
            }
        }

        return target;
    }
}